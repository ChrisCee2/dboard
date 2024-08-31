using Avalonia.Controls;
using Avalonia.Interactivity;
using dboard.ViewModels;

namespace dboard.Views;

public partial class AppSettingsView : UserControl
{
    public AppSettingsView()
    {
        InitializeComponent();
    }

    protected void ToggleThemeAccent(object sender, RoutedEventArgs e)
    {
        ((AppSettingsViewModel)DataContext).SharedSettings.UseThemeAccent = !((AppSettingsViewModel)DataContext).SharedSettings.UseThemeAccent;
    }
}