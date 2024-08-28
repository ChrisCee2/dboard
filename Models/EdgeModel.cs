using CommunityToolkit.Mvvm.ComponentModel;
using mystery_app.Constants;

namespace mystery_app.Models;

public partial class EdgeModel : ObservableObject
{
    public EdgeModel() {}

    public EdgeModel(
        NodeModelBase fromNode,
        NodeModelBase toNode)
    {
        FromNode = fromNode;
        ToNode = toNode;
        Description = null;
        Thickness = EdgeConstants.DEFAULT_THICKNESS;
        A = EdgeConstants.DEFAULT_COLOR.Color.A;
        R = EdgeConstants.DEFAULT_COLOR.Color.R;
        G = EdgeConstants.DEFAULT_COLOR.Color.G;
        B = EdgeConstants.DEFAULT_COLOR.Color.B;
    }

    public EdgeModel(
        NodeModelBase fromNode, 
        NodeModelBase toNode, 
        string description,
        byte a, byte r, byte g, byte b,
        double thickness = EdgeConstants.DEFAULT_THICKNESS)
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

    [ObservableProperty]
    private NodeModelBase _fromNode;
    [ObservableProperty]
    private NodeModelBase _toNode;
    [ObservableProperty]
    private string? _description;
    [ObservableProperty]
    private double _thickness;
    [ObservableProperty]
    private byte _a;
    [ObservableProperty]
    private byte _r;
    [ObservableProperty]
    private byte _g;
    [ObservableProperty]
    private byte _b;
}
