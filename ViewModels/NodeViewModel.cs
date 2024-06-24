using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;

namespace mystery_app.ViewModels;
    
public partial class NodeViewModel : ObservableObject
{

    public NodeViewModel()
    {
        _name = "";
        _desc = "";
        _edges = new Dictionary<NodeViewModel, string>();
    }

    public NodeViewModel(string name="", string desc="", Dictionary<NodeViewModel, string> edges=null)
    {
        _name = name;
        _desc = desc;
        if (edges != null) 
        {
            _edges = edges;
        }
        else
        {
            _edges = new Dictionary<NodeViewModel, string>();
        }
    }

    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private string _desc;

    [ObservableProperty]
    private Dictionary<NodeViewModel, string> _edges;
}