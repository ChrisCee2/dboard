using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Messages;

namespace mystery_app.ViewModels;

public partial class MainContentViewModel : ObservableObject
{
    public WorkspaceViewModel Workspace { get; set; }

    public MainContentViewModel(string backgroundColor)
    {
        Workspace = new WorkspaceViewModel(backgroundColor);
    }

    [RelayCommand]
    private void CreateNode()
    {
        WeakReferenceMessenger.Default.Send(new CreateNodeMessage(""));
    }

    [RelayCommand]
    private void GoToSettings()
    {
        WeakReferenceMessenger.Default.Send(new ChangePageMessage(Constants.Pages.SETTINGS));
    }
}
