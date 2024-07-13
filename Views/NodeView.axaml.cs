using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.VisualTree;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Controls;
using mystery_app.Messages;
using mystery_app.Models;
using mystery_app.ViewModels;

namespace mystery_app.Views;

public partial class NodeView : MovableUserControl
{
    private bool _resizing;
    private Point _lastPressedPoint;

    public NodeView()
    {
        InitializeComponent();
    }

    protected override void OnDataContextEndUpdate()
    {
        base.OnDataContextEndUpdate();
        if (DataContext != null)
        {
            var position = ((NodeViewModel)DataContext).Position;
            LoadTransform(new TranslateTransform(position.X, position.Y));
        }
    }

    public override void OnTransform(TranslateTransform transform) 
    {
        var message = new MoveNodeHelper((NodeViewModel)DataContext, new Point(transform.X, transform.Y));
        WeakReferenceMessenger.Default.Send(new MoveNodeMessage(message));
    }

    // On selecting node edge creation, tell workspace this node has been selected
    private void SelectNodeEdge(object sender, PointerPressedEventArgs args)
    {
        WeakReferenceMessenger.Default.Send(new SelectNodeEdgeMessage((NodeViewModel)DataContext));
    }

    // On releasing node edge creation, find node released on and send to workspace
    private void ReleaseNodeEdge(object sender, PointerReleasedEventArgs args)
    {
        var root = (TopLevel)((Visual)args.Source).GetVisualRoot();
        var rootCoordinates = args.GetPosition(root);
        var hitElements = root.GetInputElementsAt(rootCoordinates);

        foreach (var hitElement in hitElements)
        {
            if (hitElement is Control control && control.Tag == "EdgeButton")
            {
                WeakReferenceMessenger.Default.Send(new ReleaseNodeEdgeMessage((NodeViewModel)control.DataContext));
            }
        }
    }

    // On selecting resize button, get current cursor position
    private void SelectResize(object sender, PointerPressedEventArgs args)
    {
        var root = (TopLevel)((Visual)args.Source).GetVisualRoot();
        _resizing = true;
        _lastPressedPoint = args.GetPosition(root);
    }

    // On releasing resize button, get new size from difference between initial and current cursor position
    private void ReleaseResize(object sender, PointerReleasedEventArgs args)
    {
        if (_resizing)
        {
            _resizing = false;
            var root = (TopLevel)((Visual)args.Source).GetVisualRoot();
            var resizeSize = args.GetPosition(root) - _lastPressedPoint;
            ((NodeViewModel)DataContext).Width = (Width + resizeSize.X < this.MinWidth) ? this.MinWidth : Width + resizeSize.X;
            ((NodeViewModel)DataContext).Height = (Height + resizeSize.Y < this.MinHeight) ? this.MinHeight : Height + resizeSize.Y;
        }
    }
}