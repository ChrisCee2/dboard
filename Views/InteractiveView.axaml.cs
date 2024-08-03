using Avalonia.Input;
using Avalonia.Media;
using Avalonia;
using mystery_app.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Messages;
using Avalonia.Controls;
using System;
using mystery_app.Constants;
using Avalonia.VisualTree;
using Avalonia.Logging;
using Avalonia.LogicalTree;

namespace mystery_app.Views;

public partial class InteractiveView : Grid
{
    // Move variables
    private bool _isMoving;
    private Point _positionInBlock;
    private TranslateTransform? _transform = new TranslateTransform(0, 0);
    // Resize variables
    private bool _isResizing;
    private NodeConstants.RESIZE _resizeAxis;
    private Point _lastPosition;
    private double _lastWidth;
    private double _lastHeight;

    public InteractiveView()
    {
        InitializeComponent();
    }

    protected override void OnDataContextEndUpdate()
    {
        base.OnDataContextEndUpdate();
        if (DataContext != null)
        {
            // Render position of control on loading data context
            var position = ((NodeViewModelBase)DataContext).Position;
            _transform = new TranslateTransform(position.X, position.Y);
            RenderTransform = new TranslateTransform(position.X, position.Y);

            // Handle isSelected
            NodeViewModelBase viewModel = (NodeViewModelBase)this.DataContext;
            viewModel.PropertyChanged += (sender, args) =>
            {
                if (!args.PropertyName.Equals("IsSelected"))
                    return;
                var isSelected = viewModel.IsSelected;
                if (isSelected)
                {
                    if (!WeakReferenceMessenger.Default.IsRegistered<MoveNodeMessage>(this))
                    {
                        WeakReferenceMessenger.Default.Register<MoveNodeMessage>(this, (sender, message) =>
                        {
                                var offsetX = ((NodeViewModelBase)DataContext).Position.X + message.Value.X;
                                var offsetY = ((NodeViewModelBase)DataContext).Position.Y + message.Value.Y;
                                _moveControl(offsetX, offsetY);
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

    protected void MovePointerPressed(object sender, PointerPressedEventArgs e)
    {
        var clickProperties = e.GetCurrentPoint(Parent as Visual).Properties;

        if (clickProperties.IsRightButtonPressed)
        {
            WeakReferenceMessenger.Default.Send(new SelectNodeMessage((NodeViewModelBase)DataContext));
        }
        else if (clickProperties.IsLeftButtonPressed)
        {
            // If double click, make click through
            if (e.ClickCount >= 2)
            {
                ((NodeViewModelBase)DataContext).IsEdit = true;
                WeakReferenceMessenger.Default.Send(new SelectNodeMessage((NodeViewModelBase)DataContext));
                return;
            }
            WeakReferenceMessenger.Default.Send(new SelectNodeMessage((NodeViewModelBase)DataContext));

            _isMoving = true;
            var pos = e.GetPosition((Visual?)Parent);
            _positionInBlock = new Point(pos.X - (int)_transform.X, pos.Y - (int)_transform.Y);
        }
    }

    protected void MovePointerReleased(object sender, PointerReleasedEventArgs e)
    {
        _isMoving = false;
    }

    protected void MovePointerMoved(object sender, PointerEventArgs e)
    {
        if (Parent == null || !_isMoving) { return; }
        var currentPosition = e.GetPosition((Visual?)Parent);

        var offsetX = currentPosition.X - _positionInBlock.X;
        var offsetY = currentPosition.Y - _positionInBlock.Y;

        WeakReferenceMessenger.Default.Send(new MoveNodeMessage(
            new Point(offsetX - ((NodeViewModelBase)DataContext).Position.X, offsetY - ((NodeViewModelBase)DataContext).Position.Y))
        );
    }

    // On selecting resize button, get current cursor position and set axis to resize
    private void ResizePointerPressed(object sender, PointerPressedEventArgs e)
    {
        // If not left click, return
        if (!e.GetCurrentPoint(Parent as Visual).Properties.IsLeftButtonPressed) { return; }
        _isResizing = true;
        _resizeAxis = (NodeConstants.RESIZE)System.Enum.Parse(typeof(NodeConstants.RESIZE), ((Control)sender).Tag.ToString());
        var pos = e.GetPosition((Visual?)Parent);
        _positionInBlock = new Point(pos.X - (int)_transform.X, pos.Y - (int)_transform.Y);
        _lastPosition = pos;
        _lastWidth = ((NodeViewModelBase)DataContext).Width;
        _lastHeight = ((NodeViewModelBase)DataContext).Height;
    }

    protected void ResizePointerReleased(object sender, PointerReleasedEventArgs e)
    {
        _isResizing = false;
    }

    protected void ResizePointerMoved(object sender, PointerEventArgs e)
    {
        if (Parent == null || !_isResizing) { return; }
        Point currentPosition = e.GetPosition((Visual?)Parent);
        Point resizeDir = NodeConstants.RESIZE_TO_DIR[_resizeAxis];
        NodeViewModelBase context = ((NodeViewModelBase)DataContext);

        if (_isResizing)
        {
            // Offset found by subtracting original cursor position on resize press from the current cursor position
            double offsetX = (currentPosition.X - _lastPosition.X) * resizeDir.X;
            double offsetY = (currentPosition.Y - _lastPosition.Y) * resizeDir.Y;

            context.Width = Math.Max(_lastWidth + offsetX, NodeConstants.MIN_WIDTH);
            context.Height = Math.Max(_lastHeight + offsetY, NodeConstants.MIN_HEIGHT);
            if ((int)_resizeAxis > 2)
            {
                var moveOffsetX = (_lastPosition.X - _positionInBlock.X) + ((context.Width - _lastWidth) * resizeDir.X);
                var moveOffsetY = (_lastPosition.Y - _positionInBlock.Y) + ((context.Height - _lastHeight) * resizeDir.Y);
                if (_resizeAxis == NodeConstants.RESIZE.xY) { moveOffsetY = _transform.Y; } // Don't move on Y axis if resizing left-down
                if (_resizeAxis == NodeConstants.RESIZE.Xy) { moveOffsetX = _transform.X; } // Don't move on X axis if resizing up-right
                _moveControl(moveOffsetX, moveOffsetY);
            }
        }
    }

    private void _moveControl(double offsetX, double offsetY)
    {
        _transform = new TranslateTransform(offsetX, offsetY);
        RenderTransform = _transform;

        // Update position in viewmodel when node is moved
        ((NodeViewModelBase)DataContext).Position = new Point(offsetX, offsetY);
    }

    // On selecting node edge creation, tell workspace this node has been selected
    protected void SelectNodeEdge(object sender, PointerPressedEventArgs e)
    {
        var clickProperties = e.GetCurrentPoint(Parent as Visual).Properties;
        if (!clickProperties.IsLeftButtonPressed) { return; }

        WeakReferenceMessenger.Default.Send(new SelectNodeMessage((NodeViewModelBase)DataContext));
        WeakReferenceMessenger.Default.Send(new SelectNodeEdgeMessage((NodeViewModelBase)DataContext));
    }

    // On releasing node edge creation, find node released on and send to workspace
    protected void ReleaseNodeEdge(object sender, PointerReleasedEventArgs e)
    {
        var root = (TopLevel)((Visual)e.Source).GetVisualRoot();
        var rootCoordinates = e.GetPosition(root);
        var hitElement = root.InputHitTest(rootCoordinates);
        if (((Visual)hitElement).FindLogicalAncestorOfType<InteractiveView>() is InteractiveView node)
        {
            WeakReferenceMessenger.Default.Send(new ReleaseNodeEdgeMessage((NodeViewModelBase)node.DataContext));
        }
    }
}