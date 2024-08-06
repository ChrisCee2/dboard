using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using mystery_app.Models;

namespace mystery_app.Views;

public partial class ImageView : Panel
{
    public ImageView()
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
            ((NodeModel)DataContext).ImagePath = files[0].Path.LocalPath;
        }
    }

    private bool _IsImage(string url)
    {
        return Constants.ImageConstants.IMAGE_URL_REGEX.IsMatch(url);
    }
}