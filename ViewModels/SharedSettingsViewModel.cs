using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

    [RelayCommand]
    private void ToggleControl()
    {
        if (ModeModel is ToggleModeModel modeModel)
        {
            if (modeModel.InControl)
            {
                ((ToggleModeModel)ModeModel).ToggleProperties(
                    Constants.SettingsConstants.TRANSPARENT_BACKGROUND_COLOR,
                    Constants.SettingsConstants.TRANSPARENT_ITEM_OPACITY,
                    Constants.SettingsConstants.TRANSPARENT_WORKSPACE_OPACITY);
            }
            else
            {
                ((ToggleModeModel)ModeModel).ToggleProperties(
                    UserModeModel.Background,
                    UserModeModel.ItemOpacity,
                    UserModeModel.WorkspaceOpacity);
            }

            ((ToggleModeModel)ModeModel).InControl = !((ToggleModeModel)ModeModel).InControl;
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
    // TODO: Refactor the toggle logic, it's kind of eh and it can probably be improved
    // TODO: Refactor into a Model, probably fits better as one
    // TODO: Refactor "Default" into a constant
}