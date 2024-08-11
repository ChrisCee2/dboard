using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace mystery_app.Models;

[JsonDerivedType(typeof(NodeModel), typeDiscriminator: "")]
public abstract partial class NodeModelBase : ObservableObject
{
    [ObservableProperty]
    private string _desc;

    [ObservableProperty]
    private double _width;

    [ObservableProperty]
    private double _height;

    [ObservableProperty]
    private double _positionX;

    [ObservableProperty]
    private double _positionY;

    [ObservableProperty]
    private int _zIndex;
}
