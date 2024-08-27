using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Constants;
using mystery_app.Messages;
using mystery_app.Models;

namespace mystery_app.ViewModels;

public partial class MainContentViewModel : ObservableObject
{
    [ObservableProperty]
    public WorkspaceViewModel _workspace;
    [ObservableProperty]
    public AppSettingsViewModel _settings;
    [ObservableProperty]
    public NotesModel _notes;
    [ObservableProperty]
    private SettingsModel _sharedSettings;
    [ObservableProperty]
    private string? _workspaceFileName;

    public MainContentViewModel(SettingsModel sharedSettings)
    {
        SharedSettings = sharedSettings;
        Workspace = new WorkspaceViewModel(sharedSettings);
        Settings = new AppSettingsViewModel(sharedSettings);
        Notes = new NotesModel();
        WorkspaceFileName = null;

    }

    [RelayCommand]
    private void GoToSettings()
    {
        WeakReferenceMessenger.Default.Send(new ChangePageMessage(PageConstants.PAGE.Settings));
    }

    [RelayCommand]
    private void New()
    {
        foreach (var node in Workspace.Nodes)
        {
            // Unregisters all nodes. TODO: Improve this, there has to be a better way to unregister everything
            node.IsSelected = false;
        }

        Workspace = new WorkspaceViewModel(SharedSettings);
        Notes = new NotesModel();
        WorkspaceFileName = null;
    }
}
