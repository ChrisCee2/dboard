using CommunityToolkit.Mvvm.ComponentModel;
using dboard.ViewModels;

namespace dboard.Models;


public abstract partial class NodeActionModelBase : ActionModelBase
{
    [ObservableProperty]
    private NodeViewModelBase _node;

    public NodeActionModelBase(NodeViewModelBase node)
    {
        Node = node;
    }
}
