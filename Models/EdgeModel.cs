using mystery_app.Constants;

namespace mystery_app.Models;

public class EdgeModel
{
    public EdgeModel() {}

    public EdgeModel(
        NodeModelBase fromNode, 
        NodeModelBase toNode, 
        string description, 
        double thickness, 
        byte a=EdgeConstants.A, byte r=EdgeConstants.R, byte g=EdgeConstants.G, byte b=EdgeConstants.B)
    {
        FromNode = fromNode;
        ToNode = toNode;
        Description = description;
        Thickness = thickness;
        A = a;
        R = r;
        G = g;
        B = b;
    }


    private NodeModelBase _fromNode;
    private NodeModelBase _toNode;
    private string _description;
    private double _thickness;
    private byte _a;
    private byte _r;
    private byte _g;
    private byte _b;

    public NodeModelBase FromNode
    {
        get { return _fromNode; }
        set { _fromNode = value; }
    }
    public NodeModelBase ToNode
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
    public byte A
    {
        get { return _a; }
        set { _a = value; }
    }
    public byte R
    {
        get { return _r; }
        set { _r = value; }
    }
    public byte G
    {
        get { return _g; }
        set { _g = value; }
    }
    public byte B
    {
        get { return _b; }
        set { _b = value; }
    }
}
