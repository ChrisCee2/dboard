using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using Avalonia.VisualTree;
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
            Title = "Select Image",
            AllowMultiple = false
        });

        if (files.Count == 1)
        {
            if (((Control)sender).FindAncestorOfType<StackPanel>().Name == "CanvasImage")
            {
                ((WorkspaceViewModel)DataContext).CanvasImagePath = files[0].Path.LocalPath;
            }
            else if (((Control)sender).FindAncestorOfType<StackPanel>().Name == "WorkspaceImage")
            {
                ((WorkspaceViewModel)DataContext).WorkspaceImagePath = files[0].Path.LocalPath;
            }
            else if (((Control)sender).FindAncestorOfType<StackPanel>().Name == "WindowImage")
            {
                ((WorkspaceViewModel)DataContext).WindowImagePath = files[0].Path.LocalPath;
            }
        }
    }

    public void RemoveImage(object sender, RoutedEventArgs args)
    {
        if (((Control)sender).FindAncestorOfType<StackPanel>().Name == "CanvasImage")
        {
            ((WorkspaceViewModel)DataContext).CanvasImagePath = null;
        }
        else if (((Control)sender).FindAncestorOfType<StackPanel>().Name == "WorkspaceImage")
        {
            ((WorkspaceViewModel)DataContext).WorkspaceImagePath = null;
        }
        else if (((Control)sender).FindAncestorOfType<StackPanel>().Name == "WindowImage")
        {
            ((WorkspaceViewModel)DataContext).WindowImagePath = null;
        }
    }
}