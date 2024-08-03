using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Constants;
using mystery_app.Messages;
using mystery_app.Models;

namespace mystery_app.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty]
    private SharedSettingsModel _sharedSettings;
    [ObservableProperty]
    private Collection<ModeModel> _modes = new Collection<ModeModel>();

    public SettingsViewModel(SharedSettingsModel sharedSettings)
    {
        SharedSettings = sharedSettings;
        Modes.Add(sharedSettings.UserModeModel);
        Modes.Add(new ToggleModeModel(SettingsConstants.TRANSPARENT_MODE, sharedSettings.UserModeModel));
    }

    [RelayCommand]
    private void GoToMainContent()
    {
        WeakReferenceMessenger.Default.Send(new ChangePageMessage(PageConstants.PAGE.MainContent));
    }
}