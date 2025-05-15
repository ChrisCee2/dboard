using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using dboard.Constants;
using dboard.Messages;
using dboard.Models;

namespace dboard.ViewModels;

public partial class AppSettingsViewModel : ObservableObject
{
    [ObservableProperty]
    private SettingsModel _sharedSettings;
    [ObservableProperty]
    private Color _background;
    [ObservableProperty]
    private Color _accent;
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
            SharedSettings.UserModeModel.BackgroundA, 
            SharedSettings.UserModeModel.BackgroundR, 
            SharedSettings.UserModeModel.BackgroundG, 
            SharedSettings.UserModeModel.BackgroundB);
        Accent = new Color(
            SharedSettings.UserModeModel.AccentA,
            SharedSettings.UserModeModel.AccentR,
            SharedSettings.UserModeModel.AccentG,
            SharedSettings.UserModeModel.AccentB);
    }

    [RelayCommand]
    private void GoToMainContent()
    {
        WeakReferenceMessenger.Default.Send(new ChangePageMessage(PageConstants.PAGE.MainContent));
    }

    partial void OnBackgroundChanged(Color value)
    {
        SharedSettings.UserModeModel.BackgroundA = value.A;
        SharedSettings.UserModeModel.BackgroundR = value.R;
        SharedSettings.UserModeModel.BackgroundG = value.G;
        SharedSettings.UserModeModel.BackgroundB = value.B;
    }

    partial void OnAccentChanged(Color value)
    {
        SharedSettings.UserModeModel.AccentA = value.A;
        SharedSettings.UserModeModel.AccentR = value.R;
        SharedSettings.UserModeModel.AccentG = value.G;
        SharedSettings.UserModeModel.AccentB = value.B;
    }
}