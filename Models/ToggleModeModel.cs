using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace mystery_app.Models;


public partial class ToggleModeModel : ModeModel
{
    public ToggleModeModel(string mode, Color background, double itemOpacity, double workspaceOpacity, string windowState) : base(mode, background, itemOpacity, workspaceOpacity, windowState)
    {
        Mode = mode;
        Background = background;
        ItemOpacity = itemOpacity;
        WorkspaceOpacity = workspaceOpacity;
        WindowState = windowState;
    }

    [ObservableProperty]
    private bool _inControl = false;

    public void ToggleProperties(Color background, double itemOpacity, double workspaceOpacity)
    {
        Background = background;
        ItemOpacity = itemOpacity;
        WorkspaceOpacity = workspaceOpacity;
    }
}
