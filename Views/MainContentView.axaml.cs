using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Platform.Storage;
using mystery_app.Models;
using mystery_app.ViewModels;

namespace mystery_app.Views;

public partial class MainContentView : UserControl
{
    JsonSerializerOptions options = new()
    {
        ReferenceHandler = ReferenceHandler.Preserve,
        WriteIndented = true
    };

    FilePickerFileType jsonFileType = new FilePickerFileType("json") { Patterns = new[] { "*.json" } };

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

            List<NodeModelBase> nodes = ((MainContentViewModel)DataContext).Workspace.Nodes.Select(x => x.NodeBase).ToList();
            List<EdgeModel> edges = ((MainContentViewModel)DataContext).Workspace.Edges.Select(x => x.Edge).ToList();
            WorkspaceModel workspace = new WorkspaceModel(nodes, edges);
            JsonSerializer.SerializeAsync(stream, workspace, options);
        }
    }

    protected async void Open(object sender, PointerReleasedEventArgs e)
    {
        if (!Directory.Exists("./Workspaces"))
        {
            Directory.CreateDirectory("./Workspaces");
        }

        IStorageFolder directory = await TopLevel.GetTopLevel(this).StorageProvider.TryGetFolderFromPathAsync("./Workspaces");

        var files = await TopLevel.GetTopLevel(this).StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Workspace",
            AllowMultiple = false,
            SuggestedStartLocation = directory,
            FileTypeFilter = new[] { jsonFileType }
        });

        if (files.Count == 1)
        {
            ((MainContentViewModel)DataContext).NewCommand.Execute(null);
            await using var stream = await files[0].OpenReadAsync();
            WorkspaceModel workspace = JsonSerializer.Deserialize<WorkspaceModel>(stream, options);
            foreach (var node in workspace.Nodes)
            {
                if (node is NodeModel nodeModel)
                {
                    ((MainContentViewModel)DataContext).Workspace.Nodes.Add(new NodeViewModel(nodeModel));
                }
            }
            foreach (EdgeModel edgeModel in workspace.Edges)
            {
                ((MainContentViewModel)DataContext).Workspace.Edges.Add(new EdgeViewModel(edgeModel));
            }
        }
    }
}