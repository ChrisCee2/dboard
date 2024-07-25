using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Messages;

namespace mystery_app.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty]
    private SharedSettingsViewModel _sharedSettings;

    public SettingsViewModel(SharedSettingsViewModel sharedSettings)
    {
        _sharedSettings = sharedSettings;
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
    private void ChangeColor(Color color)
    {
        SharedSettings.BackgroundColor = color;
    }
}