using System.Collections.Generic;

namespace mystery_app.Models;

public class WorkspaceModel
{
    public WorkspaceModel() {}

    public WorkspaceModel(List<NodeModelBase> nodes, List<EdgeModel> edges, NotesModel notes, int x, int y, string canvasImagePath)
    {
        Nodes = nodes;
        Edges = edges;
        Notes = notes;
        CanvasSizeX = x; 
        CanvasSizeY = y;
        CanvasImagePath = canvasImagePath;
    }

    private List<NodeModelBase> _nodes;
    private List<EdgeModel> _edges;
    private NotesModel _notes;
    private int _canvasSizeX;
    private int _canvasSizeY;
    private string _canvasImagePath;

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
    public int CanvasSizeX
    {
        get { return _canvasSizeX; }
        set { _canvasSizeX = value; }
    }
    public int CanvasSizeY
    {
        get { return _canvasSizeY; }
        set { _canvasSizeY = value; }
    }
    public string CanvasImagePath
    {
        get { return _canvasImagePath; }
        set { _canvasImagePath = value; }
    }
}
