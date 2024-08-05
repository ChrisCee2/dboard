using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using mystery_app.Models;

namespace mystery_app.Views;

public partial class DragDropImageView : Panel
{
    public DragDropImageView()
    {
        InitializeComponent();
        AddHandler(DragDrop.DropEvent, DropImage);
    }

    public async Task DropImage(object sender, DragEventArgs e)
    {
        if (e.Data.GetText() is string url && _IsImage(e.Data.GetText()))
        {
            ((NodeModel)DataContext).ImagePath = e.Data.GetText();
        }
        else if (e.Data.GetFileNames() is { } fileNames && fileNames is not null)
        {
            foreach (var file in fileNames)
            {
                if (_IsImage(file))
                {
                    ((NodeModel)DataContext).ImagePath = file;
                }
            }
        }
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
            ((NodeModel)DataContext).ImagePath = files[0].Path.LocalPath;
        }
    }

    private async Task<Stream> _GetImageStreamFromPath(string path)
    {
        return File.OpenRead(path);
    }

    private bool _IsImage(string url)
    {
        return Constants.ImageConstants.IMAGE_URL_REGEX.IsMatch(url);
    }
}