using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia;

namespace mystery_app.Controls;

public class MovableUserControl : UserControl
{

    private bool _isPressed;
    private Point _positionInBlock;
    private TranslateTransform? _transform = null;

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        // If not left click, return
        if (!e.GetCurrentPoint(Parent as Avalonia.Visual).Properties.IsLeftButtonPressed) { return; }

        _isPressed = true;
        _positionInBlock = e.GetPosition((Avalonia.Visual?)Parent);

        if (_transform != null)
        {
            _positionInBlock = new Point(_positionInBlock.X - (int)_transform.X, _positionInBlock.Y - (int)_transform.Y);
        }

        base.OnPointerPressed(e);
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

        var currentPosition = e.GetPosition((Avalonia.Visual?)Parent);

        var offsetX = currentPosition.X - _positionInBlock.X;
        var offsetY = currentPosition.Y - _positionInBlock.Y;
        _transform = new TranslateTransform(offsetX, offsetY);
        RenderTransform = _transform;

        base.OnPointerMoved(e);
    }

}