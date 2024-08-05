using CommunityToolkit.Mvvm.ComponentModel;
using mystery_app.Constants;

namespace mystery_app.Models;

public partial class SettingsModel : ObservableObject
{
    [ObservableProperty]
    private string _theme = SettingsConstants.THEMES[0];
    [ObservableProperty]
    private bool _showNotes = true;
    [ObservableProperty]
    private ModeModel _userModeModel = SettingsConstants.DEFAULT_MODE;
    [ObservableProperty]
    private ModeModel _modeModel;

    public SettingsModel()
    {
        ModeModel = UserModeModel;
    }

    public SettingsModel(ModeModel userModeModel)
    {
        UserModeModel = userModeModel;
        ModeModel = UserModeModel;
    }
}