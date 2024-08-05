using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace mystery_app.Models;

[JsonDerivedType(typeof(ModeModelToggle), typeDiscriminator: "Toggle")]
public partial class ModeModel: ObservableObject
{
    public ModeModel() {}

    public ModeModel(string name, bool showItems, double workspaceOpacity, string windowState, byte a, byte r, byte g, byte b)
    {
        Name = name;
        ShowItems = showItems;
        WorkspaceOpacity = workspaceOpacity;
        WindowState = windowState;
        A = a;
        R = r;
        G = g;
        B = b;
    }

    [ObservableProperty]
    private string _name;
    [ObservableProperty]
    private bool _showItems;
    [ObservableProperty]
    private double _workspaceOpacity;
    [ObservableProperty]
    private string _windowState;
    [ObservableProperty]
    private byte _a;
    [ObservableProperty]
    private byte _r;
    [ObservableProperty]
    private byte _g;
    [ObservableProperty]
    private byte _b;
}
