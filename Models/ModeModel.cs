using System.Text.Json.Serialization;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace dboard.Models;

[JsonDerivedType(typeof(ModeModelToggle), typeDiscriminator: "Toggle")]
public partial class ModeModel: ObservableObject
{
    public ModeModel() {}

    public ModeModel(string name, bool showItems, double workspaceOpacity, string windowState, Color background, Color accent)
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
}
