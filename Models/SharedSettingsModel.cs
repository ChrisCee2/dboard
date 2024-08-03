using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using mystery_app.Constants;

namespace mystery_app.Models;

public partial class SharedSettingsModel : ObservableObject
{
    [ObservableProperty]
    private string _theme = SettingsConstants.THEMES[0];
    [ObservableProperty]
    private bool _showNotes = true;
    [ObservableProperty]
    private ModeModel _userModeModel = SettingsConstants.DEFAULT_MODE;
    [ObservableProperty]
    private ModeModel _modeModel;

    public SharedSettingsModel()
    {
        ModeModel = UserModeModel;
    }

    [RelayCommand]
    private void ToggleMode()
    {
        if (ModeModel is ToggleModeModel modeModel)
        {
            ((ToggleModeModel)ModeModel).Toggle();
        }
    }

    [RelayCommand]
    private void ToggleFullScreen()
    {
        if (ModeModel == UserModeModel)
        {
            if (UserModeModel.WindowState == "FullScreen")
            {
                UserModeModel.WindowState = "Normal";
            }
            else if (UserModeModel.WindowState == "Normal")
            {
                UserModeModel.WindowState = "FullScreen";
            }
        }
    }
}