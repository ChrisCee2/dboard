using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia;
using Avalonia.VisualTree;
using mystery_app.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Messages;
using System;
using mystery_app.Models;

namespace mystery_app.Controls;

public abstract class NodeUserControl : UserControl
{
    // Move variables
    private bool _isMoving;
    private Point _positionInBlock;
    private TranslateTransform? _transform = new TranslateTransform(0, 0);
    // Resizing variables
    private bool _resizing;
    private double _lastWidth;
    private double _lastHeight;

    protected override void OnDataContextEndUpdate()
    {
        base.OnDataContextEndUpdate();
        if (DataContext != null)
        {
            // Render position of node on loading data context
            var position = ((NodeViewModelBase)DataContext).Position;
            _transform = new TranslateTransform(position.X, position.Y);
            RenderTransform = new TranslateTransform(position.X, position.Y);

            // Handle isSelected
            NodeViewModelBase viewModel = (NodeViewModelBase)this.DataContext;
            viewModel.PropertyChanged += (sender, args) => {
                if (!args.PropertyName.Equals("IsSelected"))
                    return;
                var isSelected = viewModel.IsSelected;
                if (isSelected)
                {
                    if (!WeakReferenceMessenger.Default.IsRegistered<MoveNodeMessage>(this))
                    {
                        WeakReferenceMessenger.Default.Register<MoveNodeMessage>(this, (sender, message) =>
                        {
                            var offsetX = ((NodeViewModelBase)DataContext).Position.X + message.Value.Offset.X;
                            var offsetY = ((NodeViewModelBase)DataContext).Position.Y + message.Value.Offset.Y;
                            _transform = new TranslateTransform(offsetX, offsetY);
                            RenderTransform = _transform;

                            // Update position in viewmodel when node is moved
                            ((NodeViewModelBase)DataContext).Position = new Point(offsetX, offsetY);
                        });
                    }
                }
                else
                {
                    if (WeakReferenceMessenger.Default.IsRegistered<MoveNodeMessage>(this))
                    {
                        WeakReferenceMessenger.Default.Unregister<MoveNodeMessage>(this);
                    }
                }
            };
        }
    }

    // On selecting node edge creation, tell workspace this node has been selected
    protected void SelectNodeEdge(object sender, PointerPressedEventArgs args)
    {
        WeakReferenceMessenger.Default.Send(new SelectNodeEdgeMessage((NodeViewModelBase)DataContext));
    }

    // On releasing node edge creation, find node released on and send to workspace
    protected void ReleaseNodeEdge(object sender, PointerReleasedEventArgs args)
    {
        var root = (TopLevel)((Visual)args.Source).GetVisualRoot();
        var rootCoordinates = args.GetPosition(root);
        var hitElement = root.InputHitTest(rootCoordinates);
        if (hitElement is Control control && control.Tag == Constants.NodeConstants.EDGE_BUTTON_TAG)
        {
            WeakReferenceMessenger.Default.Send(new ReleaseNodeEdgeMessage((NodeViewModelBase)control.DataContext));
        }
    }

    // On selecting resize button, get current cursor position
    protected void SelectResize(object sender, PointerPressedEventArgs args)
    {
        // If not left click, return
        if (!args.GetCurrentPoint(Parent as Visual).Properties.IsLeftButtonPressed) { return; }

        _resizing = true;
        var pos = args.GetPosition((Visual?)Parent);
        _positionInBlock = new Point(pos.X - (int)_transform.X, pos.Y - (int)_transform.Y);
        _lastWidth = ((NodeViewModelBase)DataContext).Width;
        _lastHeight = ((NodeViewModelBase)DataContext).Height;
    }

    protected override void OnPointerPressed(PointerPressedEventArgs args)
    {
        // If not left click, return
        if (!args.GetCurrentPoint(Parent as Visual).Properties.IsLeftButtonPressed) { return; }

        // If the pressed element does not have a nonmovable tag, start moving
        if (args.Source is Control control && !Constants.NodeConstants.NONMOVABLE_TAGS.Contains((string)control.Tag))
        {
            _isMoving = true;
            var pos = args.GetPosition((Visual?)Parent);
            _positionInBlock = new Point(pos.X - (int)_transform.X, pos.Y - (int)_transform.Y);
        }

        base.OnPointerPressed(args);
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        _isMoving = false;
        _resizing = false;
        base.OnPointerReleased(e);
    }

    protected override void OnPointerMoved(PointerEventArgs e)
    {
        if (Parent == null) { return; }
        var currentPosition = e.GetPosition((Visual?)Parent);

        if (_resizing)
        {
            // Offset found by subtracting original cursor position on resize press from the current cursor position
            var offsetX = currentPosition.X - (_positionInBlock.X + _transform.X);
            var offsetY = currentPosition.Y - (_positionInBlock.Y + _transform.Y);
            ((NodeViewModelBase)DataContext).Width = Math.Max(_lastWidth + offsetX, MinWidth);
            ((NodeViewModelBase)DataContext).Height = Math.Max(_lastHeight + offsetY, MinHeight);
        }
        else if (_isMoving)
        {
            var offsetX = currentPosition.X - _positionInBlock.X;
            var offsetY = currentPosition.Y - _positionInBlock.Y;
            _transform = new TranslateTransform(offsetX, offsetY);
            RenderTransform = _transform;

            base.OnPointerMoved(e);
            WeakReferenceMessenger.Default.Send(
                new MoveNodeMessage(
                    new MoveNodeModel(
                        (NodeViewModelBase)DataContext, 
                        new Point(offsetX - ((NodeViewModelBase)DataContext).Position.X, offsetY - ((NodeViewModelBase)DataContext).Position.Y)
                        )
                    )
                );

            // Update position in viewmodel when node is moved
            ((NodeViewModelBase)DataContext).Position = new Point(offsetX, offsetY);
        }
    }
}