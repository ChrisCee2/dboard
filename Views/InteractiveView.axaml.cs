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
using Avalonia.LogicalTree;
using mystery_app.Models;
using System.Threading.Tasks;

namespace mystery_app.Views;

public partial class InteractiveView : Grid
{
    private NodeViewModelBase _vm;
    // Move variables
    private bool _isMoving;
    private Point _positionInBlock;
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
            _vm = (NodeViewModelBase)DataContext;
            _vm.Control = this;
            // Set drag drop image
            if (_vm is NodeViewModel nodeVM)
            {
                Grid adorner = this.FindControl<Grid>("AdornerGrid");
                DragDrop.SetAllowDrop(this, true);
                DragDrop.SetAllowDrop(adorner, true);
                adorner.AddHandler(DragDrop.DropEvent, DropImage);
                AddHandler(DragDrop.DropEvent, DropImage);

            }
        }
    }

    protected void MovePointerPressed(object sender, PointerPressedEventArgs e)
    {
        var clickProperties = e.GetCurrentPoint(Parent as Visual).Properties;

        if (clickProperties.IsRightButtonPressed)
        {
            WeakReferenceMessenger.Default.Send(new SelectNodeMessage(_vm));
        }
        else if (clickProperties.IsLeftButtonPressed)
        {
            // If double click, make click through
            if (e.ClickCount >= 2)
            {
                WeakReferenceMessenger.Default.Send(new EditNodeMessage(_vm));
                return;
            }
            WeakReferenceMessenger.Default.Send(new SelectNodeMessage(_vm));

            _isMoving = true;
            var pos = e.GetPosition((Visual?)Parent);
            _positionInBlock = new Point(pos.X - (int)_vm.NodeBase.PositionX, pos.Y - (int)_vm.NodeBase.PositionY);
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
            new Point(offsetX - _vm.NodeBase.PositionX, offsetY - _vm.NodeBase.PositionY)
        ));
    }

    // On selecting resize button, get current cursor position and set axis to resize
    private void ResizePointerPressed(object sender, PointerPressedEventArgs e)
    {
        // If not left click, return
        if (!e.GetCurrentPoint(Parent as Visual).Properties.IsLeftButtonPressed) { return; }
        _isResizing = true;
        _resizeAxis = (NodeConstants.RESIZE)Enum.Parse(typeof(NodeConstants.RESIZE), ((Control)sender).Tag.ToString());
        var pos = e.GetPosition((Visual?)Parent);
        _positionInBlock = new Point(pos.X - (int)_vm.NodeBase.PositionX, pos.Y - (int)_vm.NodeBase.PositionY);
        _lastPosition = pos;
        _lastWidth = _vm.NodeBase.Width;
        _lastHeight = _vm.NodeBase.Height;
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

        if (_isResizing)
        {
            // Offset found by subtracting original cursor position on resize press from the current cursor position
            double offsetX = (currentPosition.X - _lastPosition.X) * resizeDir.X;
            double offsetY = (currentPosition.Y - _lastPosition.Y) * resizeDir.Y;

            _vm.NodeBase.Width = Math.Max(_lastWidth + offsetX, NodeConstants.MIN_WIDTH);
            _vm.NodeBase.Height = Math.Max(_lastHeight + offsetY, NodeConstants.MIN_HEIGHT);
            if ((int)_resizeAxis > 2)
            {
                var moveOffsetX = (_lastPosition.X - _positionInBlock.X) + ((_vm.NodeBase.Width - _lastWidth) * resizeDir.X);
                var moveOffsetY = (_lastPosition.Y - _positionInBlock.Y) + ((_vm.NodeBase.Height - _lastHeight) * resizeDir.Y);
                if (_resizeAxis == NodeConstants.RESIZE.xY) { moveOffsetY = _vm.NodeBase.PositionY; } // Don't move on Y axis if resizing left-down
                if (_resizeAxis == NodeConstants.RESIZE.Xy) { moveOffsetX = _vm.NodeBase.PositionX; } // Don't move on X axis if resizing up-right
                _moveControl(moveOffsetX, moveOffsetY);
            }
        }
    }

    private void _moveControl(double offsetX, double offsetY)
    {
        _vm.NodeBase.PositionX = offsetX;
        _vm.NodeBase.PositionY = offsetY;
    }

    // On selecting node edge creation, tell workspace this node has been selected
    protected void SelectNodeEdge(object sender, PointerPressedEventArgs e)
    {
        var clickProperties = e.GetCurrentPoint(Parent as Visual).Properties;
        if (!clickProperties.IsLeftButtonPressed) { return; }

        WeakReferenceMessenger.Default.Send(new SelectNodeMessage(_vm));
        WeakReferenceMessenger.Default.Send(new CreateNodeEdgeMessage(_vm));
    }

    // On releasing node edge creation, find node released on and send to workspace
    protected void ReleaseNodeEdge(object sender, PointerReleasedEventArgs e)
    {
        var root = (TopLevel)((Visual)e.Source).GetVisualRoot();
        var rootCoordinates = e.GetPosition(root);
        var hitElement = root.InputHitTest(rootCoordinates);
        if (((Visual)hitElement).FindLogicalAncestorOfType<InteractiveView>() is InteractiveView node && node.DataContext is NodeViewModelBase vm)
        {
            WeakReferenceMessenger.Default.Send(new ReleaseNodeEdgeMessage(vm));
        }
    }

    // Handle dropping image
    public async Task DropImage(object sender, DragEventArgs e)
    {
        if (_vm is NodeViewModel vm)
        {
            NodeModel node = vm.Node;
            if (e.Data.GetText() is string url && _IsImage(e.Data.GetText()))
            {
                node.ImagePath = e.Data.GetText();
            }
            else if (e.Data.GetFileNames() is { } fileNames && fileNames is not null)
            {
                foreach (var file in fileNames)
                {
                    if (_IsImage(file))
                    {
                        node.ImagePath = file;
                    }
                }
            }
        }
    }

    private bool _IsImage(string url)
    {
        return ImageConstants.IMAGE_URL_REGEX.IsMatch(url);
    }
}