using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace mystery_app.ViewModels;

public partial class DragDropImageViewModel : ObservableObject
{
    [ObservableProperty]
    private Bitmap _image;

    public DragDropImageViewModel(Bitmap image)
    {
        _image = image;
    }
}
