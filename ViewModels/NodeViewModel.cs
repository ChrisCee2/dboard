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
        return new NodeViewModel(new NodeModel(
            Node.Name,
            Node.Desc,
            Node.ImagePath,
            Node.Width,
            Node.Height,
            Node.PositionX,
            Node.PositionY,
            Node.Notes,
            Node.ZIndex));
    }

    public override NodeViewModelBase Clone(int zIndex)
    {
        return new NodeViewModel(new NodeModel(
            Node.Name,
            Node.Desc,
            Node.ImagePath,
            Node.Width,
            Node.Height,
            Node.PositionX,
            Node.PositionY,
            Node.Notes,
            zIndex));
    }
}