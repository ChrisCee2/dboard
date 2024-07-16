using Avalonia;
using mystery_app.ViewModels;

namespace mystery_app.Models;


public class MoveNodeHelper
{

    public MoveNodeHelper(NodeViewModelBase context, Point pos)
    {
        Context = context;
        Position = pos;
    }

    private NodeViewModelBase _context;
    private Point _position;

    public NodeViewModelBase Context
    {
        get { return _context; }
        set { _context = value; }
    }
    public Point Position
    {
        get { return _position; }
        set { _position = value; }
    }
}
