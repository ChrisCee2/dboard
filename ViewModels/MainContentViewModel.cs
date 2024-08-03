using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Constants;
using mystery_app.Messages;

namespace mystery_app.ViewModels;

public partial class MainContentViewModel : ObservableObject
{
    public WorkspaceViewModel Workspace { get; set; }
    [ObservableProperty]
    private SharedSettingsViewModel _sharedSettings;

    public MainContentViewModel(SharedSettingsViewModel sharedSettings)
    {
        _sharedSettings = sharedSettings;
        Workspace = new WorkspaceViewModel(sharedSettings);
    }

    [RelayCommand]
    private void CreateNode()
    {
        WeakReferenceMessenger.Default.Send(new CreateNodeMessage(""));
    }

    [RelayCommand]
    private void GoToSettings()
    {
        WeakReferenceMessenger.Default.Send(new ChangePageMessage(PageConstants.Page.Settings));
    }
}
