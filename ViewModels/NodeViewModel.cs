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
        Bitmap image = null,
        double width = 150,
        double height = 150,
        double x = 0,
        double y = 0)
    {
        _name = name;
        _desc = desc;
        if (image is null)
        {
            _image = new DragDropImageViewModel(new Bitmap(AssetLoader.Open(new Uri("avares://mystery_app/Assets/amongusbutt.png"))));
        }
        else
        {
            _image = new DragDropImageViewModel(image);
        }
        _width = width;
        _height = height;
        _position = new Point(x, y);
    }

    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private DragDropImageViewModel _image;

    [ObservableProperty]
    private string _notes;

    public override NodeViewModelBase Clone()
    {
        return new NodeViewModel(
            _name,
            _desc,
            _image.Image,
            _width,
            _height,
            _position.X,
            _position.Y);
    }
}