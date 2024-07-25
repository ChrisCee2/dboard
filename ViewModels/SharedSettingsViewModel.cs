using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace mystery_app.ViewModels;

public partial class SharedSettingsViewModel : ObservableObject
{
    [ObservableProperty]
    private string _theme = Constants.SettingsConstants.DEFAULT_THEME;
    [ObservableProperty]
    private Color _backgroundColor = Constants.SettingsConstants.DEFAULT_BACKGROUND_COLOR;
    [ObservableProperty]
    private bool _showNotes = true;
}