using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;

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
}