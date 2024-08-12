using System.Collections.Generic;
using Avalonia.Controls;
using System.IO;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Constants;
using mystery_app.Messages;
using mystery_app.Models;
using System;
using CommunityToolkit.Mvvm.Input;
using System.Text.Json.Serialization;

namespace mystery_app.ViewModels;

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

    public void OnWindowClosing(object sender, WindowClosingEventArgs e)
    {
        SaveSettings();
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
}
