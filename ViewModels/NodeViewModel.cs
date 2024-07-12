using System;
using System.IO;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Messages;

namespace mystery_app.ViewModels;
    
public partial class NodeViewModel : ObservableObject
{

    public NodeViewModel(string name="", string desc="", double width=150, double height=150)
    {
        _name = name;
        _desc = desc;
        _width = width;
        _height = height;
        _image = new Bitmap(AssetLoader.Open(new Uri("avares://mystery_app/Assets/amongusbutt.png")));
    }

    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private string _desc;

    [ObservableProperty]
    private Bitmap _image;

    [ObservableProperty]
    private Point _position;

    [ObservableProperty]
    private double _width;

    [ObservableProperty]
    private double _height;

    [RelayCommand]
    private void DeleteNode()
    {
        WeakReferenceMessenger.Default.Send(new DeleteNodeMessage(this));
    }
}