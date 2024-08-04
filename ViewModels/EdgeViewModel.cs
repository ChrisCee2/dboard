using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using mystery_app.Constants;
using mystery_app.Models;

namespace mystery_app.ViewModels;

public partial class EdgeViewModel : ObservableObject
{
    public EdgeViewModel(
        NodeModelBase fromNode,
        NodeModelBase toNode,
        string description = "",
        double thickness = EdgeConstants.THICKNESS)
    {
        _edge = new EdgeModel(fromNode, toNode, description, thickness, EdgeConstants.COLOR);
    }

    public EdgeViewModel(
        NodeModelBase fromNode,
        NodeModelBase toNode,
        Color color,
        string description = "",
        double thickness = EdgeConstants.THICKNESS)
    {
        _edge = new EdgeModel(fromNode, toNode, description, thickness, color);
    }

    [ObservableProperty]
    private EdgeModel _edge;

}