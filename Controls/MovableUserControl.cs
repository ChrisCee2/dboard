using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia;
using Avalonia.VisualTree;

namespace mystery_app.Controls;

public class MovableUserControl : UserControl
{

    private bool _isPressed;
    private Point _positionInBlock;
    private TranslateTransform? _transform = null;

    protected override void OnPointerPressed(PointerPressedEventArgs args)
    {
        // If not left click, return
        if (!args.GetCurrentPoint(Parent as Visual).Properties.IsLeftButtonPressed) { return; }

        // Don't start moving if tag is to not move
        var root = (TopLevel)((Visual)args.Source).GetVisualRoot();
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

    virtual public void OnTransform(TranslateTransform transform) {}

    public void LoadTransform(TranslateTransform transform)
    {
        _transform = transform;
        RenderTransform = _transform;
    }
}