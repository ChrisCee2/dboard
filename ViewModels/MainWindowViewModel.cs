using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Messages;

namespace mystery_app.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly Dictionary<string, ObservableObject> Pages = new Dictionary<string, ObservableObject>();
    [ObservableProperty]
    private ObservableObject _currentPage;
    [ObservableProperty]
    private SharedSettingsViewModel _sharedSettings = new SharedSettingsViewModel();

    public MainWindowViewModel()
    {
        // Initialize available pages
        Pages.Add(Constants.PagesConstants.SETTINGS, new SettingsViewModel(_sharedSettings));
        Pages.Add(Constants.PagesConstants.MAIN_CONTENT, new MainContentViewModel(_sharedSettings));
        _currentPage = Pages[Constants.PagesConstants.MAIN_CONTENT];

        WeakReferenceMessenger.Default.Register<ChangePageMessage>(this, (sender, message) =>
        {
            var pageName = message.Value;
            CurrentPage = Pages.ContainsKey(pageName) ? Pages[pageName] : CurrentPage;
        });
    }
}
