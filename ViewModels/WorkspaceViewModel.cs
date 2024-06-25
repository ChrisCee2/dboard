using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Messages;

namespace mystery_app.ViewModels;

public partial class WorkspaceViewModel : ViewModelBase
{

    private NodeViewModel? _selectedNode;
    private NodeViewModel? _enteredNode;

    public WorkspaceViewModel()
    {
        Nodes = new ObservableCollection<NodeViewModel>(new List<NodeViewModel>());

        WeakReferenceMessenger.Default.Register<CreateNodeMessage>(this, (sender, message) =>
        {
            CreateNode();
        });
        
        WeakReferenceMessenger.Default.Register<SelectNodeEdgeMessage>(this, (sender, message) =>
        {
            if (Nodes.Contains(message.Value))
            {
                _selectedNode = message.Value;
            }
        });
        
        WeakReferenceMessenger.Default.Register<ReleaseNodeEdgeMessage>(this, (sender, message) =>
        {
            _enteredNode = message.Value;

            if (Nodes.Contains(_enteredNode) && Nodes.Contains(_selectedNode) && _selectedNode != null && _enteredNode != null && !Object.Equals(_enteredNode, _selectedNode))
            {
                if (_selectedNode.Edges.ContainsKey(_enteredNode))
                {
                    _selectedNode.Edges.Remove(_enteredNode);
                }
                _selectedNode.Edges.Add(_enteredNode, "reason");
            }
            _selectedNode = null;
            _enteredNode = null;
        });
    }

    private void CreateNode()
    {
        Nodes.Add(new NodeViewModel());
    }

    public ObservableCollection<NodeViewModel> Nodes { get; set; }

}
