using Avalonia;
using mystery_app.ViewModels;

namespace mystery_app.Models;


public class MoveNodeModel
{
    public MoveNodeModel(NodeViewModelBase node, Point offset)
    {
        Node = node;
        Offset = offset;
    }

    private NodeViewModelBase _node;
    private Point _offset;

    public NodeViewModelBase Node
    {
        get { return _node; }
        set { _node = value; }
    }
    public Point Offset
    {
        get { return _offset; }
        set { _offset = value; }
    }
}
