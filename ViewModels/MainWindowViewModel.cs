using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Constants;
using mystery_app.Messages;
using mystery_app.Models;

namespace mystery_app.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly Dictionary<PageConstants.PAGE, ObservableObject> Pages = new Dictionary<PageConstants.PAGE, ObservableObject>();
    [ObservableProperty]
    private ObservableObject _currentPage;
    [ObservableProperty]
    private SharedSettingsModel _sharedSettings = new SharedSettingsModel();

    public MainWindowViewModel()
    {
        // Initialize available pages
        Pages.Add(PageConstants.PAGE.Settings, new SettingsViewModel(_sharedSettings));
        Pages.Add(PageConstants.PAGE.MainContent, new MainContentViewModel(_sharedSettings));
        _currentPage = Pages[PageConstants.PAGE.MainContent];

        WeakReferenceMessenger.Default.Register<ChangePageMessage>(this, (sender, message) =>
        {
            var pageName = message.Value;
            CurrentPage = Pages.ContainsKey(pageName) ? Pages[pageName] : CurrentPage;
        });
    }
}
