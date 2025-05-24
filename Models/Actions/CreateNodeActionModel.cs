using dboard.ViewModels;

namespace dboard.Models;


public partial class CreateNodeActionModel : NodeActionModelBase
{
    public CreateNodeActionModel(
        NodeViewModelBase nodeBefore, 
        NodeViewModelBase node
    ) : base(nodeBefore, node) { }

    public override void Undo(WorkspaceViewModel workspaceVM)
    {
        if (Node is not null)
        {
            workspaceVM.Nodes.Remove(Node);
            Node = null;
        }
    }

    public override void Redo(WorkspaceViewModel workspaceVM)
    {
        NodeViewModelBase NewNode = NodeAfterAction.Clone();
        if (Node is not null)
        {
            workspaceVM.Nodes.Remove(Node);
        }
        workspaceVM.Nodes.Add(NewNode);
        Node = NewNode;
    }
}