using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;

namespace mystery_app.ViewModels;
    
public partial class NodeViewModel : ObservableObject
{

    public NodeViewModel()
    {
        _name = "";
        _desc = "";
    }

    public NodeViewModel(string name="", string desc="")
    {
        _name = name;
        _desc = desc;
    }

    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private string _desc;

    [ObservableProperty]
    private Point _position;
}