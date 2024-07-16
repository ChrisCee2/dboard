using System;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Messages;

namespace mystery_app.ViewModels;

public partial class NodeViewModelBase : ObservableObject
{

    public NodeViewModelBase(
        string desc = "",
        double width = 150,
        double height = 150,
        double x = 0,
        double y = 0)
    {
        _desc = desc;
        _width = width;
        _height = height;
        _position = new Point(x, y);
    }

    [ObservableProperty]
    protected string _desc;

    [ObservableProperty]
    protected double _width;

    [ObservableProperty]
    protected double _height;

    [ObservableProperty]
    protected Point _position;

    [RelayCommand]
    protected void DeleteNode()
    {
        WeakReferenceMessenger.Default.Send(new DeleteNodeMessage(this));
    }
}