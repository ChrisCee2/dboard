using CommunityToolkit.Mvvm.ComponentModel;
using mystery_app.Models;

namespace mystery_app.ViewModels;

public partial class SharedSettingsViewModel : ObservableObject
{
    [ObservableProperty]
    private string _theme = Constants.SettingsConstants.DEFAULT_THEME;
    [ObservableProperty]
    private bool _showNotes = true;
    [ObservableProperty]
    private ModeModel _userModeModel = Constants.SettingsConstants.DEFAULT_MODE;
    [ObservableProperty]
    private ModeModel _modeModel;

    public SharedSettingsViewModel()
    {
        ModeModel = UserModeModel;
    }
}