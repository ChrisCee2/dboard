using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using dboard.ViewModels;

namespace dboard.Models;


public abstract partial class NodeActionModelBase : ObservableObject
{
    [ObservableProperty]
    private NodeViewModelBase _nodeAfterAction;
    [ObservableProperty]
    private NodeViewModelBase _nodeBeforeAction;
    [ObservableProperty]
    private NodeViewModelBase? _node;

    public NodeActionModelBase(NodeViewModelBase nodeBefore, NodeViewModelBase node)
    {
        NodeBeforeAction = nodeBefore;
        Node = node;
        NodeAfterAction = node.Clone();
    }

    abstract public void Undo(WorkspaceViewModel workspaceVM);

    abstract public void Redo(WorkspaceViewModel workspaceVM);
}
