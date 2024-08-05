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
    public ObservableCollection<NodeViewModelBase> Nodes { get; set; } = new ObservableCollection<NodeViewModelBase>();
    public EdgeCollectionModel Edges { get; set; } = new EdgeCollectionModel();
    [ObservableProperty]
    private NodeViewModelBase? _selectedNodeEdge = NodeConstants.NULL_NODEVIEWMODEL;
    [ObservableProperty]
    private Point _cursorPosition;
    [ObservableProperty]
    private Point _pressedPosition;
    [ObservableProperty]
    private int _edgeThickness;
    [ObservableProperty]
    private int _multiSelectThickness;
    [ObservableProperty]
    private bool _isMultiSelecting;
    [ObservableProperty]
    private ObservableCollection<NodeViewModelBase> _selectedNodes = new ObservableCollection<NodeViewModelBase>();
    [ObservableProperty]
    private SettingsModel _sharedSettings;
    [ObservableProperty]
    private ObservableCollection<NodeViewModelBase>? _copiedNodes;

    private bool CanPaste() => _copiedNodes != null;

    public WorkspaceViewModel(SettingsModel sharedSettings)
    {
        _sharedSettings = sharedSettings;
        
        WeakReferenceMessenger.Default.Register<SelectNodeEdgeMessage>(this, (sender, message) =>
        {
            if (message.Value is NodeViewModelBase nodeVMBase && Nodes.Contains(nodeVMBase))
            {
                EdgeThickness = EdgeConstants.THICKNESS;
                SelectedNodeEdge = message.Value;
                PressedPosition = new Point(nodeVMBase.NodeBase.PositionX, nodeVMBase.NodeBase.PositionY);
            }
        });

        WeakReferenceMessenger.Default.Register<ReleaseNodeEdgeMessage>(this, (sender, message) =>
        {
            var enteredNode = message.Value;

            if (Nodes.Contains(enteredNode) 
                && Nodes.Contains(SelectedNodeEdge)
                && !Equals(enteredNode, SelectedNodeEdge)
                && !Edges.ContainsEdge(SelectedNodeEdge.NodeBase, enteredNode.NodeBase))
            {
                Edges.Add(new EdgeViewModel(new EdgeModel(SelectedNodeEdge.NodeBase, enteredNode.NodeBase)));
            }
            SelectedNodeEdge = NodeConstants.NULL_NODEVIEWMODEL;
        });

        WeakReferenceMessenger.Default.Register<DeleteNodeMessage>(this, (sender, message) =>
        {
            // Deregisters move messenger through the property listener in InteractiveView
            foreach (var nodeVM in SelectedNodes)
            {
                nodeVM.IsSelected = false;
            }

            // Remove nodes
            Nodes.RemoveMany(SelectedNodes);

            // Remove edges
            var edgesToRemove = new Collection<EdgeViewModel>();
            foreach (EdgeViewModel edgeViewModel in Edges)
            {
                foreach (NodeViewModelBase nodeVM in SelectedNodes)
                {
                    if (nodeVM.NodeBase == edgeViewModel.Edge.FromNode || nodeVM.NodeBase == edgeViewModel.Edge.ToNode)
                    {
                        edgesToRemove.Add(edgeViewModel);
                        break;
                    }
                }
            }
            Edges.RemoveMany(edgesToRemove);

            SelectedNodes = new ObservableCollection<NodeViewModelBase>();
        });

        WeakReferenceMessenger.Default.Register<CopyNodeMessage>(this, (sender, message) =>
        {
            CopiedNodes = new ObservableCollection<NodeViewModelBase>();
            foreach (var node in SelectedNodes) {
                CopiedNodes.Add(node.Clone());
            }
        });
    }

    [RelayCommand]
    private void CreateNode()
    {
        CreateEmptyNode();
    }

    [RelayCommand(CanExecute = nameof(CanPaste))]
    private void PasteNode()
    {
        foreach (var node in CopiedNodes)
        {
            Nodes.Add(node.Clone());
        }
    }

    public void CreateEmptyNode()
    {
        Nodes.Add(new NodeViewModel());
    }
}
