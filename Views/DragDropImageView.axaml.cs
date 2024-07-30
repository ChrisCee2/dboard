using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using mystery_app.ViewModels;

namespace mystery_app.Views;

public partial class DragDropImageView : Image
{
    private static HttpClient s_httpClient = new();
    private Regex imageUrlRegex = new Regex(@"(jpg|jpeg|png|webp|avif|gif)$");

    public DragDropImageView()
    {
        InitializeComponent();
        this.AddHandler(DragDrop.DropEvent, DropImage);
    }

    public async Task DropImage(object sender, DragEventArgs e)
    {
        var formats = e.Data.GetDataFormats();

        if (formats.Contains("text/html") && _IsImageUrl(e.Data.GetText()))
        {
            await using (var imageStream = await _GetImage(e.Data.GetText()))
            {
                if (imageStream is not null) 
                {
                    ((DragDropImageViewModel)DataContext).Image = await Task.Run(() => Bitmap.DecodeToWidth(imageStream, 400));
                }
            }
        }

    }

    private async Task<Stream> _GetImage(string url)
    {
        var data = await s_httpClient.GetByteArrayAsync(url);
        return new MemoryStream(data);
    }

    private bool _IsImageUrl(string url)
    {
        return imageUrlRegex.IsMatch(url);
    }
}