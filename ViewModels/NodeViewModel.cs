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
    [ObservableProperty]
    private NodeModel _node = new NodeModel("Chris", "He's a dude", 30, 30);
}