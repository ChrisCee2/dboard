using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Messages;

namespace mystery_app.ViewModels;
    
public partial class NodeViewModel : ObservableObject
{

    public NodeViewModel(string name="", string desc="", double width=100, double height=110)
    {
        _name = name;
        _desc = desc;
        _width = width;
        _height = height;
    }

    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private string _desc;

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