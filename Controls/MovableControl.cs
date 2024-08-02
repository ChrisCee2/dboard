using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia;
using mystery_app.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Messages;
using mystery_app.Models;
using Avalonia.Controls.Primitives;

namespace mystery_app.Controls;

public class MovableControl : AdornerLayer
{
    // Move variables
    private bool _isMoving;
    private Point _positionInBlock;
    private TranslateTransform? _transform = new TranslateTransform(0, 0);

    public MovableControl()
    {
        var bc = new BrushConverter();
        Background = (IBrush)bc.ConvertFrom("#FFFF0000");
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

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        // If double click, make click through
        if (e.ClickCount >= 2)
        {
            IsHitTestVisible = false;
        }

        // If not left click, return
        if (!e.GetCurrentPoint(Parent as Visual).Properties.IsLeftButtonPressed) { return; }

        _isMoving = true;
        var pos = e.GetPosition((Visual?)Parent);
        _positionInBlock = new Point(pos.X - (int)_transform.X, pos.Y - (int)_transform.Y);

        base.OnPointerPressed(e);
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        _isMoving = false;
        base.OnPointerReleased(e);
    }

    protected override void OnPointerMoved(PointerEventArgs e)
    {
        if (Parent == null || !_isMoving) { return; }
        var currentPosition = e.GetPosition((Visual?)Parent);

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