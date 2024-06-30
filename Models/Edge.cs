using mystery_app.ViewModels;

namespace mystery_app.Models;


public class Edge
{
    public Edge(NodeViewModel fromNode, NodeViewModel toNode, string description="")
    {
        FromNode = fromNode;
        ToNode = toNode;
        Description = description;
    }

    private NodeViewModel _fromNode;
    private NodeViewModel _toNode;
    private string _description;

    public NodeViewModel FromNode
    {
        get { return _fromNode; }
        set { _fromNode = value; }
    }
    public NodeViewModel ToNode
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
