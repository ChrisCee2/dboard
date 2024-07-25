using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DynamicData;
using mystery_app.Constants;
using mystery_app.Messages;
using mystery_app.Models;

namespace mystery_app.ViewModels;

public partial class WorkspaceViewModel : ObservableObject
{
    public ObservableCollection<NodeViewModelBase> Nodes { get; set; } = new ObservableCollection<NodeViewModelBase>(new List<NodeViewModelBase>());
    public EdgeCollectionModel Edges { get; set; } = new EdgeCollectionModel();
    [ObservableProperty]
    private NodeViewModelBase? _selectedNode = NodeConstants.NULL_NODEVIEWMODEL;
    [ObservableProperty]
    private Point _cursorPosition;
    [ObservableProperty]
    private int _edgeVisualThickness;
    [ObservableProperty]
    private SharedSettingsViewModel _sharedSettings;

    public WorkspaceViewModel(SharedSettingsViewModel sharedSettings)
    {
        _sharedSettings = sharedSettings;

        WeakReferenceMessenger.Default.Register<CreateNodeMessage>(this, (sender, message) =>
        {
            Nodes.Add(new NodeViewModel());
        });
        
        WeakReferenceMessenger.Default.Register<SelectNodeEdgeMessage>(this, (sender, message) =>
        {
            if (Nodes.Contains(message.Value))
            {
                EdgeVisualThickness = EdgeConstants.THICKNESS;
                SelectedNode = message.Value;
            }
        });

        WeakReferenceMessenger.Default.Register<ReleaseNodeEdgeMessage>(this, (sender, message) =>
        {
            var enteredNode = message.Value;

            if (Nodes.Contains(enteredNode) 
                && Nodes.Contains(SelectedNode)
                && !Equals(enteredNode, SelectedNode)
                && !Edges.ContainsEdge(SelectedNode, enteredNode))
            {
                Edges.Add(new EdgeViewModel(SelectedNode, enteredNode));
            }
            SelectedNode = NodeConstants.NULL_NODEVIEWMODEL;
        });

        WeakReferenceMessenger.Default.Register<DeleteNodeMessage>(this, (sender, message) =>
        {
            var node = message.Value;

            // Remove node
            Nodes.Remove(node);

            // Remove edges
            var edgesToRemove = new Collection<EdgeViewModel>();
            foreach (EdgeViewModel edgeViewModel in Edges)
            {
                if (edgeViewModel.Edge.FromNode == node || edgeViewModel.Edge.ToNode == node)
                {
                    edgesToRemove.Add(edgeViewModel);
                }
            }
            Edges.RemoveMany(edgesToRemove);
        });
    }

    [RelayCommand]
    private void CreateNode()
    {
        WeakReferenceMessenger.Default.Send(new CreateNodeMessage(""));
    }
}
