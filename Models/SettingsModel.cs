using CommunityToolkit.Mvvm.ComponentModel;
using dboard.Constants;

namespace dboard.Models;

public partial class SettingsModel : ObservableObject
{
    [ObservableProperty]
    private string _theme = SettingsConstants.THEMES[0];
    [ObservableProperty]
    private bool _useThemeAccent = true;
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