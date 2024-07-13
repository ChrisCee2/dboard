using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Messages;

namespace mystery_app.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{

    // A read.only array of possible pages
    private readonly Dictionary<string, ObservableObject> Pages;
    [ObservableProperty]
    private ObservableObject _currentPage;
    [ObservableProperty]
    private string _currentTheme;

    public MainWindowViewModel()
    {
        _currentTheme = "Default";
        var defaultBackgroundColor = "#FFFFFF";

        Pages = new Dictionary<string, ObservableObject>();
        Pages.Add("Settings", new SettingsViewModel(_currentTheme, defaultBackgroundColor));
        Pages.Add("MainContent", new MainContentViewModel(defaultBackgroundColor));
        _currentPage = Pages["MainContent"];

        WeakReferenceMessenger.Default.Register<ChangePageMessage>(this, (sender, message) =>
        {
            Navigate(message.Value);
        });

        WeakReferenceMessenger.Default.Register<ChangeThemeMessage>(this, (sender, message) =>
        {
            CurrentTheme = message.Value;
        });
    }

    [RelayCommand]
    private void Navigate(string pageName)
    {
        if (Pages.ContainsKey(pageName))
        {
            CurrentPage = Pages[pageName];
        }
    }
}
