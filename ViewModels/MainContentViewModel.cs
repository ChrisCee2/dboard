using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Constants;
using mystery_app.Messages;
using mystery_app.Models;

namespace mystery_app.ViewModels;

public partial class MainContentViewModel : ObservableObject
{
    public WorkspaceViewModel Workspace { get; set; }
    [ObservableProperty]
    private SharedSettingsModel _sharedSettings;

    public MainContentViewModel(SharedSettingsModel sharedSettings)
    {
        _sharedSettings = sharedSettings;
        Workspace = new WorkspaceViewModel(sharedSettings);
    }

    [RelayCommand]
    private void CreateNode()
    {
        Workspace.CreateEmptyNode();
    }

    [RelayCommand]
    private void GoToSettings()
    {
        WeakReferenceMessenger.Default.Send(new ChangePageMessage(PageConstants.PAGE.Settings));
    }
}
