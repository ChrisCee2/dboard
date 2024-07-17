using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia;
using Avalonia.VisualTree;
using mystery_app.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Messages;
using mystery_app.Models;

namespace mystery_app.Controls;

public abstract class NodeUserControl : UserControl
{
    // Move variables
    private bool _isPressed;
    private Point _positionInBlock;
    private TranslateTransform? _transform = null;
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
            RenderTransform = new TranslateTransform(position.X, position.Y);
        }
    }

    // Update position in viewmodel when node is moved
    protected void OnTransform(TranslateTransform transform)
    {
        var message = new MoveNodeHelper((NodeViewModelBase)DataContext, new Point(transform.X, transform.Y));
        WeakReferenceMessenger.Default.Send(new MoveNodeMessage(message));
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
        var hitElements = root.GetInputElementsAt(rootCoordinates);

        foreach (var hitElement in hitElements)
        {
            if (hitElement is Control control && control.Tag == "EdgeButton")
            {
                WeakReferenceMessenger.Default.Send(new ReleaseNodeEdgeMessage((NodeViewModelBase)control.DataContext));
            }
        }
    }

    // On selecting resize button, get current cursor position
    protected void SelectResize(object sender, PointerPressedEventArgs args)
    {
        var root = _GetRoot((Visual)args.Source);
        _resizing = true;
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

        // Don't start moving if tag is to not move
        var root = _GetRoot((Visual)args.Source);
        var rootCoordinates = args.GetPosition(root);
        var hitElement = root.InputHitTest(rootCoordinates);

        if (hitElement is Control control)
        {
            if (control == null || control.Tag == "EdgeButton" || control.Tag == "Nonmovable") { return; }
        }

        _isPressed = true;
        _positionInBlock = args.GetPosition((Visual?)Parent);

        if (_transform != null)
        {
            _positionInBlock = new Point(_positionInBlock.X - (int)_transform.X, _positionInBlock.Y - (int)_transform.Y);
        }

        base.OnPointerPressed(args);
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        _isPressed = false;

        base.OnPointerReleased(e);
    }

    protected override void OnPointerMoved(PointerEventArgs e)
    {
        if (!_isPressed) { return; }
        if (Parent == null) { return; }

        var currentPosition = e.GetPosition((Visual?)Parent);

        var offsetX = currentPosition.X - _positionInBlock.X;
        var offsetY = currentPosition.Y - _positionInBlock.Y;
        _transform = new TranslateTransform(offsetX, offsetY);
        RenderTransform = _transform;

        OnTransform(_transform);
        base.OnPointerMoved(e);
    }

    private TopLevel _GetRoot(Visual source)
    {
        return (TopLevel)source.GetVisualRoot();
    }
}