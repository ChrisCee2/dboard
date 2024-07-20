using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace mystery_app.ViewModels;

public partial class SharedSettingsViewModel : ObservableObject
{
    [ObservableProperty]
    private string _theme = Constants.Settings.DEFAULT_THEME;
    [ObservableProperty]
    private Color _backgroundColor = Constants.Settings.DEFAULT_BACKGROUND_COLOR;
}