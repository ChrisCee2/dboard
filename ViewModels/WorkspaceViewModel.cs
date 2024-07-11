using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DynamicData;
using mystery_app.Messages;
using mystery_app.Models;

namespace mystery_app.ViewModels;

public partial class WorkspaceViewModel : ObservableObject
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

        WeakReferenceMessenger.Default.Register<DeleteNodeMessage>(this, (sender, message) =>
        {
            var node = message.Value;
            // Remove node
            Nodes.Remove(node);

            // Remove edges
            var edgesToRemove = new Collection<Edge>();
            foreach (Edge edge in Edges)
            {
                if (edge.FromNode == node || edge.ToNode == node)
                {
                    edgesToRemove.Add(edge);
                }
            }
            Edges.RemoveMany(edgesToRemove);
        });
    }

    private void CreateNode()
    {
        Nodes.Add(new NodeViewModel());
    }
}
