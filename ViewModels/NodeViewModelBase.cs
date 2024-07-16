using System;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Messages;

namespace mystery_app.ViewModels;

public abstract partial class NodeViewModelBase : ObservableObject
{
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