using mystery_app.ViewModels;

namespace mystery_app.Models;


public class Edge
{
    public Edge(NodeViewModelBase fromNode, NodeViewModelBase toNode, string description="")
    {
        FromNode = fromNode;
        ToNode = toNode;
        Description = description;
    }

    private NodeViewModelBase _fromNode;
    private NodeViewModelBase _toNode;
    private string _description;

    public NodeViewModelBase FromNode
    {
        get { return _fromNode; }
        set { _fromNode = value; }
    }
    public NodeViewModelBase ToNode
    {
        get { return _toNode; }
        set { _toNode = value; }
    }
    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }
}
