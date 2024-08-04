using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using mystery_app.ViewModels;

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
                            DataContext = await Task.Run(() => Bitmap.DecodeToWidth(imageStream, 400));
                        }
                    }
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
            await using (var imageStream = await files[0].OpenReadAsync())
            {
                if (imageStream is not null)
                {
                    DataContext = await Task.Run(() => Bitmap.DecodeToWidth(imageStream, 400));
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
}