using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Logging;
using Avalonia.Media.Imaging;
using mystery_app.ViewModels;

namespace mystery_app.Views;

public partial class DragDropImageView : Image
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
        return Constants.ImageConstants.imageUrlRegex.IsMatch(url);
    }
}