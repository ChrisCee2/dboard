using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using mystery_app.Constants;
using mystery_app.Models;

namespace mystery_app.ViewModels;

public partial class NodeViewModel : NodeViewModelBase
{
    public NodeViewModel(
        string name = "",
        string desc = "",
        string imagePath = NodeConstants.DEFAULT_IMAGE_PATH,
        double width = 150,
        double height = 150,
        double x = 0,
        double y = 0,
        string notes = "")
    {
        Node = new NodeModel(
            name, 
            desc,
            imagePath, 
            width, 
            height, 
            new Point(x, y),
            notes);
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
        return new NodeViewModel(
            Node.Name,
            Node.Desc,
            Node.ImagePath,
            Node.Width,
            Node.Height,
            Node.Position.X,
            Node.Position.Y,
            Node.Notes);
    }
}