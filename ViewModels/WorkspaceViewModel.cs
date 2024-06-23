using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Messages;

namespace mystery_app.ViewModels;

public partial class WorkspaceViewModel : ViewModelBase
{

    public WorkspaceViewModel()
    {
        Nodes = new ObservableCollection<NodeViewModel>(new List<NodeViewModel>());

        WeakReferenceMessenger.Default.Register<CreateNodeMessage>(this, (sender, message) =>
        {
            CreateNode();
        });
    }

    private void CreateNode()
    {
        Nodes.Add(new NodeViewModel());
    }

    public ObservableCollection<NodeViewModel> Nodes { get; set; }

}
