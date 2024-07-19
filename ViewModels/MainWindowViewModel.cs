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
    private SharedSettingsViewModel _sharedSettings = new SharedSettingsViewModel();

    public MainWindowViewModel()
    {
        // Initialize available pages
        Pages = new Dictionary<string, ObservableObject>();
        Pages.Add(Constants.Pages.SETTINGS, new SettingsViewModel(_sharedSettings));
        Pages.Add(Constants.Pages.MAIN_CONTENT, new MainContentViewModel(_sharedSettings));
        _currentPage = Pages[Constants.Pages.MAIN_CONTENT];

        WeakReferenceMessenger.Default.Register<ChangePageMessage>(this, (sender, message) =>
        {
            Navigate(message.Value);
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
