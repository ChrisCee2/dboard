using Avalonia.Media;
using mystery_app.ViewModels;

namespace mystery_app.Models;

public class EdgeModel
{
    public EdgeModel(NodeViewModelBase fromNode, NodeViewModelBase toNode, string description, double thickness, Color color)
    {
        FromNode = fromNode;
        ToNode = toNode;
        Description = description;
        Thickness = thickness;
        Color = color;
    }


    private NodeViewModelBase _fromNode;
    private NodeViewModelBase _toNode;
    private string _description;
    private double _thickness;
    private Color _color;

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
    public double Thickness
    {
        get { return _thickness; }
        set { _thickness = value; }
    }
    public Color Color
    {
        get { return _color; }
        set { _color = value; }
    }
}
