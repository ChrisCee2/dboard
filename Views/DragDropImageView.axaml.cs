using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Logging;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using mystery_app.ViewModels;

namespace mystery_app.Views;

public partial class DragDropImageView : Panel
{
    public DragDropImageView()
    {
        InitializeComponent();
        this.AddHandler(DragDrop.DropEvent, DropImage);
    }

    public async Task DropImage(object sender, DragEventArgs e)
    {
        var formats = e.Data.GetDataFormats();
        if (e.Data.GetFileNames() is { } fileNames && fileNames is not null)
        {
            foreach (var file in fileNames)
            {
                if (_IsImage(file))
                {
                    await using (var imageStream = await _GetImageStreamFromPath(file))
                    {
                        if (imageStream is not null)
                        {
                            ((DragDropImageViewModel)DataContext).Image = await Task.Run(() => Bitmap.DecodeToWidth(imageStream, 400));
                        }
                    }
                }
            }
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

    private async void OpenFileButton_Clicked(object sender, RoutedEventArgs args)
    {
        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(this);

        // Start async operation to open the dialog.
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Insert Image",
            AllowMultiple = false
        });

        if (files.Count == 1)
        {
            // Open reading stream from the first file.
            //await using var stream = await files[0].OpenReadAsync();
            //var fileContent = await streamReader.ReadToEndAsync();

            await using (var imageStream = await files[0].OpenReadAsync())
            {
                if (imageStream is not null)
                {
                    ((DragDropImageViewModel)DataContext).Image = await Task.Run(() => Bitmap.DecodeToWidth(imageStream, 400));
                }
            }
        }
    }
}