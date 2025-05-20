using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using dboard.Constants;
using dboard.Messages;
using dboard.Models;

namespace dboard.ViewModels;

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
    [ObservableProperty]
    private WorkspaceConstants.SAVE_STATUS _saveStatus;

    public MainContentViewModel(SettingsModel sharedSettings)
    {
        SharedSettings = sharedSettings;
        Workspace = new WorkspaceViewModel(sharedSettings);
        Settings = new AppSettingsViewModel(sharedSettings);
        Notes = new NotesModel();
        WorkspaceFileName = null;
        SaveStatus = WorkspaceConstants.SAVE_STATUS.UNSAVED;
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

    public void LoadWorkspace(WorkspaceModel newWorkspace, string workspaceName)
    {
        New();
        foreach (var node in newWorkspace.Nodes)
        {
            if (node is NodeModel nodeModel)
            {
                Workspace.Nodes.Add(new NodeViewModel(nodeModel));
            }
        }
        foreach (EdgeModel edgeModel in newWorkspace.Edges)
        {
            Workspace.Edges.Add(new EdgeViewModel(edgeModel));
        }
        Notes = newWorkspace.Notes;
        WorkspaceFileName = workspaceName;
        Workspace.CanvasSizeX = newWorkspace.CanvasSizeX;
        Workspace.CanvasSizeY = newWorkspace.CanvasSizeY;
        Workspace.WorkspaceSizeX = newWorkspace.WorkspaceSizeX;
        Workspace.WorkspaceSizeY = newWorkspace.WorkspaceSizeY;
        Workspace.CanvasImagePath = newWorkspace.CanvasImagePath;
        Workspace.WorkspaceImagePath = newWorkspace.WorkspaceImagePath;
        Workspace.WindowImagePath = newWorkspace.WindowImagePath;
        Workspace.ImagePaths = new ObservableCollection<ImagePathModel>()
            {
                Workspace.CanvasImagePath,
                Workspace.WorkspaceImagePath,
                Workspace.WindowImagePath
            };
    }

    public bool Equals(MainContentViewModel viewModel)
    {
        if (Workspace.Equals(viewModel.Workspace))
        {
            return true;
        }
        return false;
    }
}
