using System.Text.Json.Serialization;
using Avalonia;
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
    private Point _position;
}
