using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Messages;
using mystery_app.Models;

namespace mystery_app.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty]
    private SharedSettingsViewModel _sharedSettings;
    [ObservableProperty]
    private Collection<ModeModel> _modes = Constants.SettingsConstants.MODES;

    public SettingsViewModel(SharedSettingsViewModel sharedSettings)
    {
        SharedSettings = sharedSettings;
        Modes.Add(sharedSettings.UserModeModel);
    }

    [RelayCommand]
    private void GoToMainContent()
    {
        WeakReferenceMessenger.Default.Send(new ChangePageMessage(Constants.PagesConstants.MAIN_CONTENT));
    }

    [RelayCommand]
    private void ToggleNotes()
    {
        SharedSettings.ShowNotes = !SharedSettings.ShowNotes;
    }

    [RelayCommand]
    private void ChangeTheme(string theme)
    {
        SharedSettings.Theme = theme;
    }

    [RelayCommand]
    private void ChangeMode(ModeModel mode)
    {
        SharedSettings.ModeModel = mode;
    }

    [RelayCommand]
    private void ChangeColor(Color color)
    {
        SharedSettings.UserModeModel.Background = color;
    }
}