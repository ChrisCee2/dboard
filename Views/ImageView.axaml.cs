using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using dboard.ViewModels;

namespace dboard.Views;

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
            ((NodeViewModel)DataContext).Node.ImagePath = files[0].Path.LocalPath;
        }
    }
}