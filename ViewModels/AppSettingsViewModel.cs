using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Constants;
using mystery_app.Messages;
using mystery_app.Models;

namespace mystery_app.ViewModels;

public partial class AppSettingsViewModel : ObservableObject
{
    [ObservableProperty]
    private SettingsModel _sharedSettings;
    [ObservableProperty]
    private Color _background;
    [ObservableProperty]
    private Collection<ModeModel> _modes = new Collection<ModeModel>();

    public AppSettingsViewModel(SettingsModel sharedSettings)
    {
        SharedSettings = sharedSettings;
        Modes.Add(SharedSettings.UserModeModel);
        if (!SharedSettings.ModeModel.Equals(SharedSettings.UserModeModel))
        {
            Modes.Add(SharedSettings.ModeModel);
        }
        foreach (ModeModel availableMode in SettingsConstants.AVAILABLE_MODES)
        {
            bool modeFound = false;
            foreach (ModeModel mode in Modes)
            {
                if (mode.Name.Equals(availableMode.Name))
                {
                    modeFound = true;
                    break;
                }
            }
            if (!modeFound)
            {
                Modes.Add(availableMode);
            }
        }
        Background = new Color(
            SharedSettings.UserModeModel.A, 
            SharedSettings.UserModeModel.R, 
            SharedSettings.UserModeModel.G, 
            SharedSettings.UserModeModel.B);
    }

    [RelayCommand]
    private void GoToMainContent()
    {
        WeakReferenceMessenger.Default.Send(new ChangePageMessage(PageConstants.PAGE.MainContent));
    }

    partial void OnBackgroundChanged(Color value)
    {
        SharedSettings.UserModeModel.A = value.A;
        SharedSettings.UserModeModel.R = value.R;
        SharedSettings.UserModeModel.G = value.G;
        SharedSettings.UserModeModel.B = value.B;
    }
}