using System;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;

namespace mystery_app.ViewModels;

public partial class NodeViewModel : NodeViewModelBase
{

    public NodeViewModel(
        string name = "",
        string desc = "",
        string image_path = "avares://mystery_app/Assets/amongusbutt.png",
        double width = 150,
        double height = 150,
        double x = 0,
        double y = 0)
    {
        _name = name;
        _desc = desc;
        _image = new Bitmap(AssetLoader.Open(new Uri(image_path)));
        _width = width;
        _height = height;
        _position = new Point(x, y);
    }

    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private Bitmap _image;

    [ObservableProperty]
    private string _notes;
}