using Avalonia.Controls;
using Avalonia.Input;
using Avalonia;
using Avalonia.VisualTree;
using mystery_app.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Messages;

namespace mystery_app.Controls;

public abstract class NodeUserControl : UserControl
{
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
}