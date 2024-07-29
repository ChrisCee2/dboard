using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia;
using Avalonia.VisualTree;
using mystery_app.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Messages;
using System;
using Avalonia.Logging;

namespace mystery_app.Controls;

public abstract class NodeUserControl : UserControl
{
    // Move variables
    private bool _isMoving;
    private Point _positionInBlock;
    private TranslateTransform? _transform = new TranslateTransform(0, 0);
    // Resizing variables
    private bool _resizing;
    private Point _lastSize;

    // Render position of node on loading data context
    protected override void OnDataContextEndUpdate()
    {
        base.OnDataContextEndUpdate();
        if (DataContext != null)
        {
            var position = ((NodeViewModelBase)DataContext).Position;
            _transform = new TranslateTransform(position.X, position.Y);
            RenderTransform = new TranslateTransform(position.X, position.Y);
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
        _lastSize = new Point(Width, Height);

        base.OnPointerPressed(args);
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
            ((NodeViewModelBase)DataContext).Width = Math.Max(_lastSize.X + offsetX, MinWidth);
            ((NodeViewModelBase)DataContext).Height = Math.Max(_lastSize.Y + offsetY, MinHeight);
        }
        else if (_isMoving)
        {
            var offsetX = currentPosition.X - _positionInBlock.X;
            var offsetY = currentPosition.Y - _positionInBlock.Y;
            _transform = new TranslateTransform(offsetX, offsetY);
            RenderTransform = _transform;

            // Update position in viewmodel when node is moved
            ((NodeViewModelBase)DataContext).Position = new Point(offsetX, offsetY);
            base.OnPointerMoved(e);
        }
    }
}