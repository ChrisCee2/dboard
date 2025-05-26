using dboard.ViewModels;

namespace dboard.Models;


public partial class DeleteNodeActionModel : NodeActionModelBase
{
    public DeleteNodeActionModel(
        NodeViewModelBase node
    ) : base(node) { NodeBeforeHistoryAction = node.Clone(); }

    public override void Undo(WorkspaceViewModel workspaceVM)
    {
        if (NodeBeforeHistoryAction is not null)
        {
            workspaceVM.Nodes.Add(NodeBeforeHistoryAction);
            Node = NodeBeforeHistoryAction;
            NodeBeforeHistoryAction = null;
        }
    }

    public override void Redo(WorkspaceViewModel workspaceVM)
    {
        if (Node is not null)
        {
            NodeBeforeHistoryAction = Node.Clone();
            workspaceVM.Nodes.Remove(Node);
        }
    }
}