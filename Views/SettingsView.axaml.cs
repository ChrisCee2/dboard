using Avalonia.Controls;
using mystery_app.ViewModels;

namespace mystery_app.Views;

public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        InitializeComponent();
    }

    private void ChangeThemeOnSelection(object sender, SelectionChangedEventArgs e)
    {
        var theme = ((ComboBox)e.Source).SelectedItem.ToString();
        ((SettingsViewModel)DataContext).ChangeThemeCommand.Execute(theme);
    }
}