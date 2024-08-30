using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.LogicalTree;
using Avalonia.VisualTree;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Constants;
using mystery_app.Messages;
using mystery_app.ViewModels;

namespace mystery_app.Views;

public partial class EdgeCreatorView : UserControl
{
    NodeViewModelBase _vm;

    public EdgeCreatorView()
    {
        InitializeComponent();
    }

    protected override void OnDataContextEndUpdate()
    {
        base.OnDataContextEndUpdate();
        if (DataContext != null)
        {
            _vm = (NodeViewModelBase)DataContext;
        }
    }

    protected void CreateEdge(object sender, PointerPressedEventArgs e)
    {
        var clickProperties = e.GetCurrentPoint(Parent as Visual).Properties;
        if (!clickProperties.IsLeftButtonPressed) { return; }

        WeakReferenceMessenger.Default.Send(new SelectNodeMessage(_vm));
        WeakReferenceMessenger.Default.Send(new CreateNodeEdgeMessage(_vm));
    }

    // On releasing node edge creation, find node released on and send to workspace
    protected void FinishCreateEdge(object sender, PointerReleasedEventArgs e)
    {
        var root = (TopLevel)((Visual)e.Source).GetVisualRoot();
        var rootCoordinates = e.GetPosition(root);
        var hitElement = root.InputHitTest(rootCoordinates);
        if (((Visual)hitElement).FindLogicalAncestorOfType<InteractiveView>() is InteractiveView node && node.DataContext is NodeViewModelBase vm)
        {
            WeakReferenceMessenger.Default.Send(new ReleaseNodeEdgeMessage(vm));
        }
        else
        {
            WeakReferenceMessenger.Default.Send(new ReleaseNodeEdgeMessage(NodeConstants.NULL_NODEVIEWMODEL));
        }
    }
}