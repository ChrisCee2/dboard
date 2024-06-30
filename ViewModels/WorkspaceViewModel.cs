using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Messages;
using mystery_app.Models;

namespace mystery_app.ViewModels;

public partial class WorkspaceViewModel : ViewModelBase
{
    public ObservableCollection<NodeViewModel> Nodes { get; set; }
    public EdgeCollection Edges { get; set; }
    private NodeViewModel? _selectedNode;
    private NodeViewModel? _enteredNode;

    public WorkspaceViewModel()
    {
        Nodes = new ObservableCollection<NodeViewModel>(new List<NodeViewModel>());
        Edges = new EdgeCollection();

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

            if (Nodes.Contains(_enteredNode) 
                && Nodes.Contains(_selectedNode) 
                && _selectedNode != null 
                && _enteredNode != null 
                && !Object.Equals(_enteredNode, _selectedNode)
                && !Edges.ContainsEdge(_selectedNode, _enteredNode))
            {
                Edges.Add(new Edge(_selectedNode, _enteredNode, "asdf"));
            }
            _selectedNode = null;
            _enteredNode = null;
        });

        WeakReferenceMessenger.Default.Register<MoveNodeMessage>(this, (sender, message) =>
        {
            var dataContext = message.Value.Context;
            if (Nodes.Contains(dataContext))
            {
                ((NodeViewModel)dataContext).Position = message.Value.Position;
            }
        });
    }

    private void CreateNode()
    {
        Nodes.Add(new NodeViewModel());
    }
}
