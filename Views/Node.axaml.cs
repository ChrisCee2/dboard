using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.VisualTree;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Controls;
using mystery_app.Messages;
using mystery_app.Models;
using mystery_app.ViewModels;

namespace mystery_app.Views;

public partial class Node : MovableUserControl
{
    public Node()
    {
        InitializeComponent();
    }

    private void SelectNodeEdge(object sender, PointerPressedEventArgs args)
    {
        WeakReferenceMessenger.Default.Send(new SelectNodeEdgeMessage((NodeViewModel)DataContext));
    }

    private void ReleaseNodeEdge(object sender, PointerReleasedEventArgs args)
    {
        var root = (TopLevel)((Visual)args.Source).GetVisualRoot();
        var rootCoordinates = args.GetPosition(root);
        var hitElement = root.InputHitTest(rootCoordinates);

        if (hitElement is Control control)
        {
            // TODO Find node, should improve this later
            var node = control.Parent.Parent.Parent;
            if (node == null || node.DataContext == null || node.DataContext is not NodeViewModel) { return; }
            WeakReferenceMessenger.Default.Send(new ReleaseNodeEdgeMessage((NodeViewModel)node.DataContext));
        }
    }

    private void MoveNodeHandler(object sender, PointerEventArgs args)
    {
        var workspaceRoot = ((Control)((Control)sender).Parent).PointToScreen(new Point(0, 0));
        var senderRoot = ((Control)sender).PointToScreen(new Point(0, 0));
        var pos = new Point(senderRoot.X - workspaceRoot.X, senderRoot.Y - workspaceRoot.Y);
        var message = new MoveNodeHelper((NodeViewModel)DataContext, pos);
        WeakReferenceMessenger.Default.Send(new MoveNodeMessage(message));
    }
}