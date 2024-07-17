﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Logging;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using DynamicData;
using mystery_app.Messages;
using mystery_app.Models;

namespace mystery_app.ViewModels;

public partial class WorkspaceViewModel : ObservableObject
{
    public ObservableCollection<NodeViewModelBase> Nodes { get; set; }
    public EdgeCollection Edges { get; set; }
    private NodeViewModelBase? _selectedNode;
    private NodeViewModelBase? _enteredNode;
    [ObservableProperty]
    private IBrush _backgroundColor;
    BrushConverter _colorConverter;
    [ObservableProperty]
    private Point _selectedEdgeNodePosition;
    [ObservableProperty]
    private Point _cursorPosition;
    [ObservableProperty]
    private int _edgeVisualThickness;

    public WorkspaceViewModel(string backgroundColor)
    {
        Nodes = new ObservableCollection<NodeViewModelBase>(new List<NodeViewModelBase>());
        Edges = new EdgeCollection();
        _colorConverter = new BrushConverter();
        BackgroundColor = HexToBrush(backgroundColor);

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

        WeakReferenceMessenger.Default.Register<ChangeBackgroundColorMessage>(this, (sender, message) =>
        {
            BackgroundColor = HexToBrush(message.Value);
        });
    }

    private void CreateNode()
    {
        Nodes.Add(new NodeViewModel());
    }

    private IBrush HexToBrush(string hex)
    {
        return (IBrush)_colorConverter.ConvertFrom(hex);
    }
}
