using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia;
using Avalonia.VisualTree;
using mystery_app.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Messages;

namespace mystery_app.Controls;

public abstract class NodeUserControl : UserControl
{
    // Move variables
    private bool _isMoving;
    private Point _positionInBlock;
    private TranslateTransform? _transform = new TranslateTransform(0, 0);
    // Resizing variables
    private bool _resizing;
    private Point _lastPressedPoint;

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
        var root = _GetRoot((Visual)args.Source);
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
        _resizing = true;
        var root = _GetRoot((Visual)args.Source);
        _lastPressedPoint = args.GetPosition(root);
    }

    // On releasing resize button, get new size from difference between initial and current cursor position
    protected void ReleaseResize(object sender, PointerReleasedEventArgs args)
    {
        if (_resizing)
        {
            _resizing = false;
            var root = _GetRoot((Visual)args.Source);
            var resizeSize = args.GetPosition(root) - _lastPressedPoint;
            ((NodeViewModelBase)DataContext).Width = (Width + resizeSize.X < this.MinWidth) ? this.MinWidth : Width + resizeSize.X;
            ((NodeViewModelBase)DataContext).Height = (Height + resizeSize.Y < this.MinHeight) ? this.MinHeight : Height + resizeSize.Y;
        }
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
        base.OnPointerReleased(e);
    }

    protected override void OnPointerMoved(PointerEventArgs e)
    {
        if (!_isMoving || Parent == null) { return; }

        var currentPosition = e.GetPosition((Visual?)Parent);

        var offsetX = currentPosition.X - _positionInBlock.X;
        var offsetY = currentPosition.Y - _positionInBlock.Y;
        _transform = new TranslateTransform(offsetX, offsetY);
        RenderTransform = _transform;

        // Update position in viewmodel when node is moved
        ((NodeViewModelBase)DataContext).Position = new Point(offsetX, offsetY);
        base.OnPointerMoved(e);
    }

    private TopLevel _GetRoot(Visual source)
    {
        return (TopLevel)source.GetVisualRoot();
    }
}