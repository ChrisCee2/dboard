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
        A = EdgeConstants.A;
        R = EdgeConstants.R;
        G = EdgeConstants.G;
        B = EdgeConstants.B;
    }

    public EdgeModel(
        NodeModelBase fromNode, 
        NodeModelBase toNode, 
        string description, 
        double thickness = EdgeConstants.DEFAULT_THICKNESS, 
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
