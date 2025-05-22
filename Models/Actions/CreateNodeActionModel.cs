using System.Collections.ObjectModel;
using dboard.ViewModels;

namespace dboard.Models;


public partial class CreateNodeActionModel : NodeActionModelBase
{
    public CreateNodeActionModel(
        ObservableCollection<NodeViewModelBase> nodes,
        NodeViewModelBase nodeBefore, 
        NodeViewModelBase node
    ) : base(nodes, nodeBefore, node) { }

    public override void Undo()
    {
        if (Node is not null)
        {
            Nodes.Remove(Node);
            Node = null;
        }
    }

    public override void Redo()
    {
        NodeViewModelBase NewNode = NodeAfterAction.Clone();
        if (Node is not null)
        {
            Nodes.Remove(Node);
        }
        Nodes.Add(NewNode);
        Node = NewNode;
    }
}