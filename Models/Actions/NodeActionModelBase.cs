using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using dboard.ViewModels;

namespace dboard.Models;


public abstract partial class NodeActionModelBase : ObservableObject
{
    public ObservableCollection<NodeViewModelBase> Nodes;
    [ObservableProperty]
    private NodeViewModelBase _nodeAfterAction;
    [ObservableProperty]
    private NodeViewModelBase _nodeBeforeAction;
    [ObservableProperty]
    private NodeViewModelBase? _node;

    public NodeActionModelBase(ObservableCollection<NodeViewModelBase> nodes, NodeViewModelBase nodeBefore, NodeViewModelBase node)
    {
        Nodes = nodes;
        NodeBeforeAction = nodeBefore;
        Node = node;
        NodeAfterAction = node.Clone();
    }

    abstract public void Undo();

    abstract public void Redo();
}
