using CommunityToolkit.Mvvm.ComponentModel;
using dboard.ViewModels;

namespace dboard.Models;


public abstract partial class NodeActionModelBase : ObservableObject
{
    [ObservableProperty]
    private NodeViewModelBase? _node;

    public NodeActionModelBase(NodeViewModelBase node)
    {
        Node = node;
    }

    abstract public void Undo(WorkspaceViewModel workspaceVM);

    abstract public void Redo(WorkspaceViewModel workspaceVM);
}
