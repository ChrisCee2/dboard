using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Messages;

namespace mystery_app.ViewModels;

public partial class MainContentViewModel : ObservableObject
{
    public WorkspaceViewModel Workspace { get; set; }

    public MainContentViewModel(SharedSettingsViewModel sharedSettings)
    {
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
        WeakReferenceMessenger.Default.Send(new ChangePageMessage(Constants.PagesConstants.SETTINGS));
    }
}
