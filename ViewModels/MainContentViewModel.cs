using System.Collections.Generic;
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
    public NotesViewModel _notes;
    [ObservableProperty]
    private SettingsModel _sharedSettings;
    [ObservableProperty]
    private string? _workspaceFileName;
    [ObservableProperty]
    private double _notesPaneLength = WorkspaceConstants.DEFAULT_PANE_LENGTH;

    // Undo Redo properties
    [ObservableProperty]
    private List<ActionModelBase> _actionHistory = new List<ActionModelBase>();
    [ObservableProperty]
    private int _maxActions = 30;
    [ObservableProperty]
    private int _lastActionIndex = -1;

    // Save status properties
    [ObservableProperty]
    private ActionModelBase? _lastActionSinceSave = null;
    [ObservableProperty]
    private WorkspaceConstants.SAVE_STATUS _saveStatus;
    [ObservableProperty]
    private bool _isNewWorkspace = true;
    [ObservableProperty]
    private bool _shouldAlwaysBeUnsaved = false;

    // Opening workspace properties
    [ObservableProperty]
    private WorkspaceModel? _workspaceToLoad = null;
    [ObservableProperty]
    private string? _workspaceNameToLoad = null;
    [ObservableProperty]
    private bool? _shouldLoadWorkspace = false;

    public MainContentViewModel(SettingsModel sharedSettings)
    {
        SharedSettings = sharedSettings;
        Workspace = new WorkspaceViewModel(sharedSettings);
        Settings = new AppSettingsViewModel(sharedSettings);
        Notes = new NotesViewModel(sharedSettings);
        //Notes = new NoteModel(
        //    new Color(
        //        sharedSettings.ModeModel.BackgroundA,
        //        sharedSettings.ModeModel.BackgroundR,
        //        sharedSettings.ModeModel.BackgroundG,
        //        sharedSettings.ModeModel.BackgroundB
        //    )
        //);
        WorkspaceFileName = null;
        SaveStatus = WorkspaceConstants.SAVE_STATUS.UNSAVED;

        // History logging
        WeakReferenceMessenger.Default.Register<LogActionMessage>(this, (sender, message) =>
        {
            LogAction(message.Value);
        });

        WeakReferenceMessenger.Default.Register<ResetActionHistoryStatesMessage>(this, (sender, message) =>
        {
            ResetActionHistoryStatesModel val = message.Value;
            ResetActionHistoryStates(val.IsSave, val.ResetActionHistory, val.ShouldAlwaysBeUnsaved, val.IsNew);
        });

        WeakReferenceMessenger.Default.Register<HistoryActionMessage>(this, (sender, message) =>
        {
            WorkspaceConstants.HISTORY_ACTION historyAction = message.Value;
            if (historyAction == WorkspaceConstants.HISTORY_ACTION.UNDO)
            {
                Undo();
            }
            else if (historyAction == WorkspaceConstants.HISTORY_ACTION.REDO)
            {
                Redo();
            }
        });
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
        Notes = new NotesViewModel(SharedSettings);
        WorkspaceFileName = null;
        WeakReferenceMessenger.Default.Send(new ResetActionHistoryStatesMessage(new ResetActionHistoryStatesModel(false, true, false, true)));
    }

    [RelayCommand]
    private void CreateNote()
    {
        Notes.CreateNote();
    }

    // Parameterized just incase we aren't loading by the stored WorkspaceToLoad variable
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
        Notes = new NotesViewModel(SharedSettings, newWorkspace.Notes);
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

        SaveStatus = WorkspaceConstants.SAVE_STATUS.SAVED;
        ResetActionHistoryStates(false, true, false, false);

        WorkspaceToLoad = null;
        WorkspaceNameToLoad = null;
        ShouldLoadWorkspace = null;
    }

    public bool Equals(MainContentViewModel viewModel)
    {
        if (Workspace.Equals(viewModel.Workspace))
        {
            return true;
        }
        return false;
    }

    // History logging starts here
    public void UpdateSaveStatus()
    {
        if (
            !ShouldAlwaysBeUnsaved && !IsNewWorkspace &&
            (
            (LastActionSinceSave is null && LastActionIndex == -1) ||
            (LastActionIndex != -1 && ActionHistory[LastActionIndex] == LastActionSinceSave)
            )
        )
        {
            SaveStatus = WorkspaceConstants.SAVE_STATUS.SAVED;
        }
        else
        {
            SaveStatus = WorkspaceConstants.SAVE_STATUS.UNSAVED;
        }
    }

    public void Undo()
    {
        if (LastActionIndex >= 0)
        {
            ActionHistory[LastActionIndex].Undo(Workspace);
            LastActionIndex -= 1;
            UpdateSaveStatus();
        }
    }

    public void Redo()
    {
        if (LastActionIndex < ActionHistory.Count - 1)
        {
            LastActionIndex += 1;
            ActionHistory[LastActionIndex].Redo(Workspace);
            UpdateSaveStatus();
        }
    }

    public void LogAction(ActionModelBase action)
    {
        // Remove undone actions
        int actionCount = ActionHistory.Count;
        for (int i = actionCount - 1; i > LastActionIndex; i--)
        {
            ActionHistory.RemoveAt(i);
        }

        // Pop least recent action if there are too many
        actionCount = ActionHistory.Count;
        for (int i = actionCount; i > MaxActions; i--)
        {
            ActionHistory.RemoveAt(0);
        }

        ActionHistory.Add(action);
        LastActionIndex = ActionHistory.Count - 1;
        UpdateSaveStatus();
    }

    public void ResetActionHistoryStates(bool isSave, bool resetActionHistory, bool shouldAlwaysBeUnsaved, bool isNew)
    {
        if (resetActionHistory)
        {
            LastActionSinceSave = null;
            ActionHistory.Clear();
            LastActionIndex = -1;
        }
        if (isSave)
        {
            if (LastActionIndex != -1)
            {
                LastActionSinceSave = ActionHistory[LastActionIndex];
            }
        }
        IsNewWorkspace = isNew;
        ShouldAlwaysBeUnsaved = shouldAlwaysBeUnsaved;
        UpdateSaveStatus();
    }

    public bool ShouldShowSaveDialog()
    {
        if (IsNewWorkspace)
        {
            // If it is a new workspace, no edits have been made / action history can be reverted to beginning and has been
            if (!ShouldAlwaysBeUnsaved && (LastActionSinceSave is null && LastActionIndex == -1))
            {
                return false;
            }
            return true;
        }
        else if (SaveStatus == WorkspaceConstants.SAVE_STATUS.UNSAVED)
        {
            return true;
        }
        return false;
    }
    // History logging ends here

    partial void OnShouldLoadWorkspaceChanged(bool? shouldLoadWorkspace)
    {
        if (shouldLoadWorkspace is true && WorkspaceToLoad != null && WorkspaceNameToLoad != null)
        {
            LoadWorkspace(WorkspaceToLoad, WorkspaceNameToLoad);
        }
    }
}
