using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using dboard.Constants;
using dboard.Messages;
using dboard.Models;
using System;
using CommunityToolkit.Mvvm.Input;
using System.Text.Json.Serialization;

namespace dboard.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    JsonSerializerOptions options = new()
    {
        ReferenceHandler = ReferenceHandler.Preserve,
        WriteIndented = true
    };

    private readonly Dictionary<PageConstants.PAGE, ObservableObject> Pages = new Dictionary<PageConstants.PAGE, ObservableObject>();
    [ObservableProperty]
    private ObservableObject _currentPage;
    [ObservableProperty]
    private SettingsModel _sharedSettings;
    [ObservableProperty]
    private bool? _shouldClose = false;

    public MainWindowViewModel()
    {
        // Initialize shared settings
        SharedSettings = LoadSettings();

        // Initialize available pages
        Pages.Add(PageConstants.PAGE.MainContent, new MainContentViewModel(SharedSettings));
        _currentPage = Pages[PageConstants.PAGE.MainContent];

        WeakReferenceMessenger.Default.Register<ChangePageMessage>(this, (sender, message) =>
        {
            var pageName = message.Value;
            CurrentPage = Pages.ContainsKey(pageName) ? Pages[pageName] : CurrentPage;
        });
    }

    public SettingsModel LoadSettings()
    {
        if (File.Exists("./Settings.json"))
        {
            using (FileStream stream = File.OpenRead("./Settings.json"))
            {
                try
                {
                    return JsonSerializer.Deserialize<SettingsModel>(stream, options);
                }
                catch (Exception e)
                {
                    return new SettingsModel();
                }
            }
        }
        else
        {
            return new SettingsModel();
        }
    }

    public async void SaveSettings()
    {
        string settings = JsonSerializer.Serialize(SharedSettings, options);
        File.WriteAllText("./Settings.json", settings);
    }

    [RelayCommand]
    private void ToggleMode()
    {
        if (SharedSettings.ModeModel is ModeModelToggle modeModel)
        {
            modeModel.Toggle();
        }
    }

    [RelayCommand]
    private void ToggleFullScreen()
    {
        if (SharedSettings.ModeModel.Equals(SharedSettings.UserModeModel))
        {
            if (SharedSettings.UserModeModel.WindowState == "FullScreen")
            {
                SharedSettings.UserModeModel.WindowState = "Normal";
            }
            else if (SharedSettings.UserModeModel.WindowState == "Normal")
            {
                SharedSettings.UserModeModel.WindowState = "FullScreen";
            }
        }
    }

    [RelayCommand]
    private void Undo()
    {
        WeakReferenceMessenger.Default.Send(new HistoryActionMessage(WorkspaceConstants.HISTORY_ACTION.UNDO));
    }

    [RelayCommand]
    private void Redo()
    {
        WeakReferenceMessenger.Default.Send(new HistoryActionMessage(WorkspaceConstants.HISTORY_ACTION.REDO));
    }

    partial void OnShouldCloseChanged(bool? value)
    {
        if (ShouldClose == true)
        {
            WeakReferenceMessenger.Default.Send(new CloseAppMessage(""));
        }
    }

    public bool ShouldShowSaveDialog()
    {
        // Try to get the main content view model
        ObservableObject? value;
        Pages.TryGetValue(PageConstants.PAGE.MainContent, out value);

        if (value is not null && value is MainContentViewModel viewModel)
        {
            if (viewModel.IsNewWorkspace)
            {
                // If it is a new workspace, no edits have been made / action history can be reverted to beginning and has been
                if (!viewModel.ShouldAlwaysBeUnsaved && (viewModel.LastActionSinceSave is null && _lastActionIndex == -1))
                {
                    return false;
                }
                return true;
            }
            else if(viewModel is not null && viewModel.SaveStatus == WorkspaceConstants.SAVE_STATUS.UNSAVED)
            {
                return true;
            }
        }
        return false;
    }
}
