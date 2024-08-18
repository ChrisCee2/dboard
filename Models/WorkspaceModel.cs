using System.Collections.Generic;

namespace mystery_app.Models;

public class WorkspaceModel
{
    public WorkspaceModel() {}

    public WorkspaceModel(List<NodeModelBase> nodes, List<EdgeModel> edges, NotesModel notes)
    {
        Nodes = nodes;
        Edges = edges;
        Notes = notes;
    }

    private List<NodeModelBase> _nodes;
    private List<EdgeModel> _edges;
    private NotesModel _notes;

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
    public NotesModel Notes
    {
        get { return _notes; }
        set { _notes = value; }
    }
}
