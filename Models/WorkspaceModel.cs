using System.Collections.Generic;

namespace mystery_app.Models;

public class WorkspaceModel
{
    public WorkspaceModel() {}

    public WorkspaceModel(List<NodeModelBase> nodes, List<EdgeModel> edges)
    {
        Nodes = nodes;
        Edges = edges;
    }

    private List<NodeModelBase> _nodes;
    private List<EdgeModel> _edges;

    public List<NodeModelBase> Nodes
    {
        get { return _nodes; }
        set { _nodes = value; }
    }
    public List<EdgeModel> Edges
    {
        get { return _edges; }
        set { _edges = value; }
    }
}
