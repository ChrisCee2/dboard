using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using mystery_app.Models;

namespace mystery_app.ViewModels;
    
public partial class NodeViewModel : ObservableObject
{

    public NodeViewModel(string name, string desc, int x, int y)
    {
        Node = new NodeModel(name, desc, x, y);
    }

    [ObservableProperty]
    private NodeModel _node;
}