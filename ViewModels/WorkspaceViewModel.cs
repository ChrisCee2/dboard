using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    private NodeViewModelBase _nodeToCreateEdge = NodeConstants.NULL_NODEVIEWMODEL;
    [ObservableProperty]
    private Point _cursorPosition;
    [ObservableProperty]
    private Point _pressedPosition;
    [ObservableProperty]
    private double _edgeThickness;
    [ObservableProperty]
    private int _multiSelectThickness;
    [ObservableProperty]
    private bool _isMultiSelecting;
    [ObservableProperty]
    private bool _multiSelectHKDown;
    [ObservableProperty]
    private ObservableCollection<NodeViewModelBase> _selectedNodes = new ObservableCollection<NodeViewModelBase>();
    [ObservableProperty]
    private ObservableCollection<EdgeViewModel> _selectedEdges = new ObservableCollection<EdgeViewModel>();
    [ObservableProperty]
    private SettingsModel _sharedSettings;
    [ObservableProperty]
    private ObservableCollection<NodeViewModelBase> _copiedNodes = new ObservableCollection<NodeViewModelBase>();
    [ObservableProperty]
    private ObservableCollection<EdgeViewModel> _copiedEdges = new ObservableCollection<EdgeViewModel>();

    private bool CanPaste() => CopiedNodes.Count > 0;
    private bool ItemsAreSelected() => SelectedNodes.Count > 0 || SelectedEdges.Count > 0;
    private bool NodesAreSelected() => SelectedNodes.Count > 0;

    public WorkspaceViewModel(SettingsModel sharedSettings)
    {
        _sharedSettings = sharedSettings;

        WeakReferenceMessenger.Default.Register<EditNodeMessage>(this, (sender, message) =>
        {
            if (message.Value is NodeViewModelBase nodeVMBase)
            {
                _UpdateSelectedEdges(new ObservableCollection<EdgeViewModel>());
                _UpdateSelectedNodes(new ObservableCollection<NodeViewModelBase>() { nodeVMBase });
                nodeVMBase.IsEdit = true;
            }
        });

        WeakReferenceMessenger.Default.Register<CreateNodeEdgeMessage>(this, (sender, message) =>
        {
            if (message.Value is NodeViewModelBase nodeVMBase && Nodes.Contains(nodeVMBase))
            {
                EdgeThickness = EdgeConstants.THICKNESS;
                NodeToCreateEdge = nodeVMBase;
                // Manually assign position to where pin is
                CursorPosition = new Point(NodeToCreateEdge.NodeBase.PositionX + (NodeToCreateEdge.NodeBase.Width / 2), NodeToCreateEdge.NodeBase.PositionY + 11);
            }
        });

        WeakReferenceMessenger.Default.Register<ReleaseNodeEdgeMessage>(this, (sender, message) =>
        {
            var enteredNode = message.Value;

            if (Nodes.Contains(enteredNode) 
                && Nodes.Contains(NodeToCreateEdge)
                && !Equals(enteredNode, NodeToCreateEdge)
                && !Edges.ContainsEdge(NodeToCreateEdge.NodeBase, enteredNode.NodeBase))
            {
                Edges.Add(new EdgeViewModel(new EdgeModel(NodeToCreateEdge.NodeBase, enteredNode.NodeBase)));
            }
            NodeToCreateEdge = NodeConstants.NULL_NODEVIEWMODEL;
        });

        WeakReferenceMessenger.Default.Register<DeleteMessage>(this, (sender, message) =>
        {
            DeleteSelectedItems();
        });

        WeakReferenceMessenger.Default.Register<CopyNodeMessage>(this, (sender, message) =>
        {
            CopyNodes();
        });
    }

    [RelayCommand]
    private void CreateNodeAtPress()
    {
        CreateNodeAtPos(PressedPosition.X, PressedPosition.Y);
    }

    public void CreateNodeAtPos(double x, double y)
    {
        NodeModel nodeModel = new NodeModel(Nodes.Count);
        nodeModel.PositionX = x;
        nodeModel.PositionY = y;
        Nodes.Add(new NodeViewModel(nodeModel));
    }

    [RelayCommand]
    private void CreateNode()
    {
        _CreateEmptyNode();
    }

    [RelayCommand(CanExecute = nameof(CanPaste))]
    private void PasteNodes()
    {
        // Clone nodes
        IDictionary<NodeModelBase, NodeModelBase> refToClone = new Dictionary<NodeModelBase, NodeModelBase>();
        foreach (var node in CopiedNodes)
        {
            var clone = node.Clone(Nodes.Count);
            refToClone.Add(node.NodeBase, clone.NodeBase);
            Nodes.Add(clone);
        }
        // Clone edges
        foreach (EdgeViewModel edgeViewModel in CopiedEdges)
        {
            if (refToClone.ContainsKey(edgeViewModel.Edge.FromNode) && refToClone.ContainsKey(edgeViewModel.Edge.ToNode))
            {
                Edges.Add(edgeViewModel.CloneWithNewNodes(refToClone[edgeViewModel.Edge.FromNode], refToClone[edgeViewModel.Edge.ToNode]));
            }
        }
    }

    [RelayCommand(CanExecute = nameof(NodesAreSelected))]
    private void CopyNodes()
    {
        // Clear clipboard
        CopiedNodes = new ObservableCollection<NodeViewModelBase>();
        CopiedEdges = new ObservableCollection<EdgeViewModel>();

        // Copy nodes
        HashSet<NodeModelBase> copiedNodeBases = new HashSet<NodeModelBase>();
        foreach (var node in SelectedNodes)
        {
            CopiedNodes.Add(node);
            copiedNodeBases.Add(node.NodeBase);
        }
       
        // Reorder nodes
        CopiedNodes = new ObservableCollection<NodeViewModelBase>(CopiedNodes.OrderBy(nodevm => nodevm.NodeBase.ZIndex).ToList());

        // Copy edges
        foreach (EdgeViewModel edgeViewModel in Edges)
        {
            if (copiedNodeBases.Contains(edgeViewModel.Edge.FromNode) && copiedNodeBases.Contains(edgeViewModel.Edge.ToNode))
            {
                CopiedEdges.Add(edgeViewModel);
            }
        }
    }

    private void _CreateEmptyNode()
    {
        Nodes.Add(new NodeViewModel(new NodeModel(Nodes.Count)));
    }

    private void _DeleteNodes()
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

        // Reorder z indexes
        foreach (var nodeVM in Nodes)
        {
            var decrement = 0;
            foreach (var deletedNodeVM in SelectedNodes)
            {
                if (nodeVM.NodeBase.ZIndex > deletedNodeVM.NodeBase.ZIndex)
                {
                    decrement++;
                }
            }
            nodeVM.NodeBase.ZIndex -= decrement;
        }

        SelectedNodes = new ObservableCollection<NodeViewModelBase>();
    }

    private void _DeleteEdges()
    {
        // Remove edges
        Edges.RemoveMany(SelectedEdges);
        SelectedEdges = new ObservableCollection<EdgeViewModel>();
    }

    [RelayCommand(CanExecute = nameof(ItemsAreSelected))]
    private void DeleteSelectedItems()
    {
        _DeleteNodes();
        _DeleteEdges();
    }

    private void _UpdateSelectedNodes(ObservableCollection<NodeViewModelBase> nodesToSelect)
    {
        foreach (var nodeVM in SelectedNodes)
        {
            nodeVM.IsSelected = false;
            nodeVM.IsEdit = false;
        }

        foreach (NodeViewModelBase nodeVM in nodesToSelect)
        {
            nodeVM.IsSelected = true;
            nodeVM.IsEdit = false;
        }

        // Reorder z indexes
        nodesToSelect = new ObservableCollection<NodeViewModelBase>(nodesToSelect.OrderBy(nodevm => nodevm.NodeBase.ZIndex).ToList());
        foreach (var nodeVM in Nodes)
        {
            if (!nodesToSelect.Contains(nodeVM))
            {
                var decrement = 0;
                foreach (var selectedNode in nodesToSelect)
                {
                    if (nodeVM.NodeBase.ZIndex > selectedNode.NodeBase.ZIndex)
                    {
                        decrement++;
                    }
                }
                nodeVM.NodeBase.ZIndex -= decrement;
            }
        }
        foreach (var nodeVM in nodesToSelect)
        {
            nodeVM.NodeBase.ZIndex = Nodes.Count - (nodesToSelect.Count - nodesToSelect.IndexOf(nodeVM));
        }

        SelectedNodes = nodesToSelect;
    }

    private void _UpdateSelectedEdges(ObservableCollection<EdgeViewModel> edgesToSelect)
    {
        foreach (var edgeVM in SelectedEdges)
        {
            edgeVM.IsSelected = false;
        }

        foreach (EdgeViewModel edgeVM in edgesToSelect)
        {
            edgeVM.IsSelected = true;
        }

        SelectedEdges = edgesToSelect;
    }

    public void UpdateSelection(ObservableCollection<NodeViewModelBase>? nodesToSelect = null, ObservableCollection<EdgeViewModel>? edgesToSelect = null)
    {
        nodesToSelect = nodesToSelect is null ? new ObservableCollection<NodeViewModelBase> () : nodesToSelect;
        edgesToSelect = edgesToSelect is null ? new ObservableCollection<EdgeViewModel>() : edgesToSelect;
        if (MultiSelectHKDown)
        {
            if (nodesToSelect.Count == 1 && edgesToSelect.Count == 0 && SelectedNodes.Contains(nodesToSelect[0]))
            {
                SelectedNodes.Remove(nodesToSelect[0]);
                nodesToSelect[0].IsSelected = false;
                nodesToSelect[0].IsEdit = false;
                return;
            }
            else if (edgesToSelect.Count == 1 && nodesToSelect.Count == 0 && SelectedEdges.Contains(edgesToSelect[0]))
            {
                SelectedEdges.Remove(edgesToSelect[0]);
                edgesToSelect[0].IsSelected = false;
                return;
            }

            nodesToSelect = new ObservableCollection<NodeViewModelBase>(nodesToSelect.Union(SelectedNodes));
            edgesToSelect = new ObservableCollection<EdgeViewModel>(edgesToSelect.Union(SelectedEdges));
        }
        _UpdateSelectedNodes(nodesToSelect);
        _UpdateSelectedEdges(edgesToSelect);
    }
}
