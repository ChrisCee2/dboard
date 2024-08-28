using System.Collections.Generic;

namespace mystery_app.Models;

public class WorkspaceModel
{
    public WorkspaceModel() {}

    public WorkspaceModel(List<NodeModelBase> nodes, List<EdgeModel> edges, NotesModel notes, int canvasX, int canvasY, int workspaceX, int workspaceY, string canvasImagePath, string workspaceImagePath, string windowImagePath)
    {
        Nodes = nodes;
        Edges = edges;
        Notes = notes;
        CanvasSizeX = canvasX; 
        CanvasSizeY = canvasY;
        WorkspaceSizeX = workspaceX;
        WorkspaceSizeY = workspaceY;
        CanvasImagePath = canvasImagePath;
        WorkspaceImagePath = workspaceImagePath;
        WindowImagePath = windowImagePath;
    }

    private List<NodeModelBase> _nodes;
    private List<EdgeModel> _edges;
    private NotesModel _notes;
    private int _canvasSizeX;
    private int _canvasSizeY;
    private int _workspaceSizeX;
    private int _workspaceSizeY;
    private string _canvasImagePath;
    private string _workspaceImagePath;
    private string _windowImagePath;

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
    public int WorkspaceSizeX
    {
        get { return _workspaceSizeX; }
        set { _workspaceSizeX = value; }
    }
    public int WorkspaceSizeY
    {
        get { return _workspaceSizeY; }
        set { _workspaceSizeY = value; }
    }
    public string CanvasImagePath
    {
        get { return _canvasImagePath; }
        set { _canvasImagePath = value; }
    }
    public string WorkspaceImagePath
    {
        get { return _workspaceImagePath; }
        set { _workspaceImagePath = value; }
    }
    public string WindowImagePath
    {
        get { return _windowImagePath; }
        set { _windowImagePath = value; }
    }
}
