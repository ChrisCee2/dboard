using System.Text.Json.Serialization;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace dboard.Models;

[JsonDerivedType(typeof(ModeModelToggle), typeDiscriminator: "Toggle")]
public partial class ModeModel: ObservableObject
{
    public ModeModel() {}

    public ModeModel(string name, bool showItems, double workspaceOpacity, string windowState, Color background, Color accent, Color canvas, Color defaultNode)
    {
        Name = name;
        ShowItems = showItems;
        WorkspaceOpacity = workspaceOpacity;
        WindowState = windowState;
        BackgroundA = background.A;
        BackgroundR = background.R;
        BackgroundG = background.G;
        BackgroundB = background.B;
        AccentA = accent.A;
        AccentR = accent.R;
        AccentG = accent.G;
        AccentB = accent.B;
        CanvasA = canvas.A;
        CanvasR = canvas.R;
        CanvasG = canvas.G;
        CanvasB = canvas.B;
        DefaultNodeA = defaultNode.A;
        DefaultNodeR = defaultNode.R;
        DefaultNodeG = defaultNode.G;
        DefaultNodeB = defaultNode.B;
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
    private byte _backgroundA;
    [ObservableProperty]
    private byte _backgroundR;
    [ObservableProperty]
    private byte _backgroundG;
    [ObservableProperty]
    private byte _backgroundB;
    [ObservableProperty]
    private byte _accentA;
    [ObservableProperty]
    private byte _accentR;
    [ObservableProperty]
    private byte _accentG;
    [ObservableProperty]
    private byte _accentB;
    [ObservableProperty]
    private byte _canvasA;
    [ObservableProperty]
    private byte _canvasR;
    [ObservableProperty]
    private byte _canvasG;
    [ObservableProperty]
    private byte _canvasB;
    [ObservableProperty]
    private byte _defaultNodeA;
    [ObservableProperty]
    private byte _defaultNodeR;
    [ObservableProperty]
    private byte _defaultNodeG;
    [ObservableProperty]
    private byte _defaultNodeB;
}
