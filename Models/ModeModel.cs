using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace mystery_app.Models;


public partial class ModeModel: ObservableObject
{
    public ModeModel(string mode, Color background, double itemOpacity, double workspaceOpacity, string windowState)
    {
        Mode = mode;
        Background = background;
        ItemOpacity = itemOpacity;
        WorkspaceOpacity = workspaceOpacity;
        WindowState = windowState;
    }

    [ObservableProperty]
    private string _mode;
    [ObservableProperty]
    private Color _background;
    [ObservableProperty]
    private double _itemOpacity;
    [ObservableProperty]
    private double _workspaceOpacity;
    [ObservableProperty]
    private string _windowState;
}
