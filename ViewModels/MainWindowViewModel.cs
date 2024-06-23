using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Messages;

namespace mystery_app.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{

    public MainWindowViewModel()
    {
        Workspace = new WorkspaceViewModel();
    }

    public WorkspaceViewModel Workspace { get; set; }

    [RelayCommand]
    private void CreateNode()
    {
        WeakReferenceMessenger.Default.Send(new CreateNodeMessage("unused string"));
    }

}
