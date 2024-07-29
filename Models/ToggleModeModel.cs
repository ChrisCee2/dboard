using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;

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
    }

    [ObservableProperty]
    private bool _inControl = false;

    public void ToggleProperties(Color background, bool showItems, double workspaceOpacity)
    {
        Background = background;
        ShowItems = showItems;
        WorkspaceOpacity = workspaceOpacity;
    }
}
