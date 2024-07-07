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

    public MainWindowViewModel()
    {
        Pages = new Dictionary<string, ObservableObject>();
        Pages.Add("Settings", new SettingsViewModel());
        Pages.Add("MainContent", new MainContentViewModel());
        _currentPage = Pages["MainContent"];

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
