using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Platform.Storage;
using mystery_app.Models;
using mystery_app.ViewModels;

namespace mystery_app.Views;

public partial class MainContentView : UserControl
{
    public MainContentView()
    {
        InitializeComponent();
    }

    protected async void Save(object sender, PointerReleasedEventArgs e)
    {
        if (!Directory.Exists("./Workspaces"))
        {
            Directory.CreateDirectory("./Workspaces");
        }

        IStorageFolder directory = await TopLevel.GetTopLevel(this).StorageProvider.TryGetFolderFromPathAsync("./Workspaces");

        IStorageFile file = await TopLevel.GetTopLevel(this).StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "Save Workspace",
            SuggestedFileName = "Workspace",
            SuggestedStartLocation = directory,
            DefaultExtension = "json"
        });

        if (file is not null)
        {
            // Open writing stream from the file.
            await using var stream = await file.OpenWriteAsync();
            //using var streamWriter = new StreamWriter(stream);

            ObservableCollection<NodeViewModelBase> nodes = ((MainContentViewModel)DataContext).Workspace.Nodes;
            EdgeCollectionModel edges = ((MainContentViewModel)DataContext).Workspace.Edges;
            JsonSerializer.SerializeAsync(stream, nodes);
            JsonSerializer.SerializeAsync(stream, edges);
        }
    }
}