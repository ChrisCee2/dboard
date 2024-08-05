using CommunityToolkit.Mvvm.ComponentModel;
using mystery_app.Constants;
using mystery_app.Models;

namespace mystery_app.ViewModels;

public partial class NodeViewModel : NodeViewModelBase
{
    public NodeViewModel()
    {
        Node = new NodeModel(
            "",
            "",
            NodeConstants.DEFAULT_IMAGE_PATH,
            150,
            150,
            0,
            0,
            "");
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
            Node.Notes));
    }
}