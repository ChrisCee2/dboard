using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using mystery_app.ViewModels;

namespace mystery_app.Views;

public partial class WorkspaceSettingsView : UserControl
{
    public WorkspaceSettingsView()
    {
        InitializeComponent();
    }

    public async void OpenFileButton_Clicked(object sender, RoutedEventArgs args)
    {
        var files = await TopLevel.GetTopLevel(this).StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Insert Image",
            AllowMultiple = false
        });

        if (files.Count == 1)
        {
            ((WorkspaceViewModel)DataContext).CanvasImagePath = files[0].Path.LocalPath;
        }
    }

    public void RemoveImage(object sender, RoutedEventArgs args)
    {
        ((WorkspaceViewModel)DataContext).CanvasImagePath = null;
    }
}