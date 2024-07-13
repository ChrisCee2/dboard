using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Messages;

namespace mystery_app.ViewModels;

public partial class SettingsViewModel : ObservableObject
{

    public ObservableCollection<string> Themes { get; set; }

    [ObservableProperty]
    private string _currentTheme;
    [ObservableProperty]
    private Color _currentBackgroundColor;
    private System.Drawing.ColorConverter _colorConverter;

    public SettingsViewModel(string currentTheme, string backgroundColor)
    {
        _colorConverter = new System.Drawing.ColorConverter();
        CurrentTheme = currentTheme;
        System.Drawing.Color tempColor = (System.Drawing.Color)_colorConverter.ConvertFromString(backgroundColor);
        CurrentBackgroundColor = new Color(tempColor.A, tempColor.R, tempColor.G, tempColor.B);
        Themes = new ObservableCollection<string>();
        Themes.Add("Default");
        Themes.Add("Light");
        Themes.Add("Dark");
    }

    [RelayCommand]
    private void GoToMainContent()
    {
        WeakReferenceMessenger.Default.Send(new ChangePageMessage("MainContent"));
    }

    [RelayCommand]
    private void ChangeTheme(string theme)
    {
        CurrentTheme = theme;
        WeakReferenceMessenger.Default.Send(new ChangeThemeMessage(CurrentTheme));
    }

    [RelayCommand]
    private void ChangeColor(Color color)
    {
        CurrentBackgroundColor = color;
        var hexString = _colorConverter.ConvertToString(CurrentBackgroundColor);
        WeakReferenceMessenger.Default.Send(new ChangeBackgroundColorMessage(hexString));
    }
}