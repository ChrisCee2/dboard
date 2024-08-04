using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Logging;
using Avalonia.Platform.Storage;
using mystery_app.Models;
using mystery_app.Tools;
using mystery_app.ViewModels;

namespace mystery_app.Views;

public partial class MainContentView : UserControl
{
    JsonSerializerOptions options = new()
    {
        WriteIndented = true
    };

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
            options.ReferenceHandler = new RefHandler();

            List<NodeModelBase> nodes = ((MainContentViewModel)DataContext).Workspace.Nodes.Select(x => x.NodeBase).ToList();
            List<EdgeModel> edges = ((MainContentViewModel)DataContext).Workspace.Edges.Select(x => x.Edge).ToList();
            foreach (var node in nodes)
            {
                JsonSerializer.SerializeAsync(stream, node, options);
            }
            foreach (var edge in edges)
            {
                JsonSerializer.SerializeAsync(stream, edge, options);
            }
        }
    }
}