using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;

namespace mystery_app.ViewModels;
    
public partial class NodeViewModel : ObservableObject
{

    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private string _desc;

    [ObservableProperty]
    private Dictionary<NodeViewModel, string> _edges;
}