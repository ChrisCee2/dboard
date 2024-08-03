using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using DynamicData;
using mystery_app.Constants;
using mystery_app.Models;

namespace mystery_app.Models;


public partial class ToggleModeModel : ModeModel
{
    public ToggleModeModel(string mode, Color background, bool showItems, double workspaceOpacity, string windowState) : base(mode, background, showItems, workspaceOpacity, windowState)
    {
        Mode = mode;
        Background = background;
        ShowItems = showItems;
        WorkspaceOpacity = workspaceOpacity;
        WindowState = windowState;
        OffMode = new ModeModel(Mode, Background, ShowItems, WorkspaceOpacity, WindowState);
        OnMode = new ModeModel(Mode, Background, ShowItems, WorkspaceOpacity, WindowState);
    }

    public ToggleModeModel(ModeModel offMode, ModeModel onMode)
    {
        OffMode = offMode;
        OnMode = onMode;

        // Settings default to off mode
        Mode = OffMode.Mode;
        Background = OffMode.Background;
        ShowItems = OffMode.ShowItems;
        WorkspaceOpacity = OffMode.WorkspaceOpacity;
        WindowState = OffMode.WindowState;
    }

    [ObservableProperty]
    private bool _isToggled = false;

    [ObservableProperty]
    private ModeModel _offMode; // Untoggled

    [ObservableProperty]
    private ModeModel _onMode; // Toggled

    public void Toggle()
    {
        IsToggled = !IsToggled;
        UpdateMode();
    }

    public void Toggle(bool isToggled)
    {
        IsToggled = isToggled;
        UpdateMode();
    }

    public void UpdateMode()
    {
        var mode = IsToggled ? OnMode : OffMode;
        Background = mode.Background;
        ShowItems = mode.ShowItems;
        WorkspaceOpacity = mode.WorkspaceOpacity;
    }
}
