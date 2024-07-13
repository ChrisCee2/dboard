using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Messages;

namespace mystery_app.ViewModels;

public partial class SettingsViewModel : ObservableObject
{

    public ObservableCollection<string> Themes { get; set; }

    [ObservableProperty]
    private string _currentTheme;

    public SettingsViewModel(string currentTheme)
    {
        _currentTheme = currentTheme;
        Themes = new ObservableCollection<string>();
        Themes.Add("Default");
        Themes.Add("Light");
        Themes.Add("Dark");
    }

    [RelayCommand]
    private void GoToMainContent()
    {
        WeakReferenceMessenger.Default.Send(new ChangePageMessage("MainContent"));
    }

    [RelayCommand]
    private void ChangeTheme(string theme)
    {
        CurrentTheme = theme;
        WeakReferenceMessenger.Default.Send(new ChangeThemeMessage(CurrentTheme));
    }
}