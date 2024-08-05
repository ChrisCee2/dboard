using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using mystery_app.Constants;
using mystery_app.Models;

namespace mystery_app.ViewModels;

public partial class EdgeViewModel : ObservableObject
{
    public EdgeViewModel(EdgeModel edgeModel)
    {
        Edge = edgeModel;
    }

    public EdgeViewModel(
        NodeModelBase fromNode,
        NodeModelBase toNode,
        string description = "",
        double thickness = EdgeConstants.THICKNESS)
    {
        Edge = new EdgeModel(
            fromNode, 
            toNode, 
            description, 
            thickness, 
            EdgeConstants.COLOR.A, EdgeConstants.COLOR.R, EdgeConstants.COLOR.G, EdgeConstants.COLOR.B);
    }

    public EdgeViewModel(
        NodeModelBase fromNode,
        NodeModelBase toNode,
        Color color,
        string description = "",
        double thickness = EdgeConstants.THICKNESS)
    {
        Edge = new EdgeModel(fromNode, toNode, description, thickness, color.A, color.R, color.G, color.B);
    }

    [ObservableProperty]
    private EdgeModel _edge;

}