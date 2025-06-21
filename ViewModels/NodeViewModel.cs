using CommunityToolkit.Mvvm.ComponentModel;
using dboard.Models;

namespace dboard.ViewModels;

public partial class NodeViewModel : NodeViewModelBase
{
    public NodeViewModel()
    {
        Node = new NodeModel();
    }

    public NodeViewModel(NodeModel node)
    {
        Node = node;
    }

    [ObservableProperty]
    private NodeModel _node;
    public override NodeModelBase NodeBase
    {
        get { return _node; }
        set { _node = (NodeModel)value; }
    }

    public override NodeViewModelBase Clone()
    {
        return new NodeViewModel(Node.Clone());
    }

    public override NodeViewModelBase Clone(int zIndex)
    {
        NodeModel node = Node.Clone();
        node.ZIndex = zIndex;
        return new NodeViewModel(node);
    }

    public override void Copy(NodeViewModelBase nodeToCopy)
    {
        NodeViewModel nodeViewModel = (NodeViewModel)nodeToCopy;
        Node.Copy(nodeViewModel.Node);
    }
}