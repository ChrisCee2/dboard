using dboard.ViewModels;

namespace dboard.Models;


public partial class CreateNodeActionModel : NodeActionModelBase
{
    public CreateNodeActionModel(
        NodeViewModelBase node
    ) : base(node) { }

    public override void Undo(WorkspaceViewModel workspaceVM)
    {
        if (Node is not null)
        {
            NodeBeforeHistoryAction = Node.Clone();
            workspaceVM.Nodes.Remove(Node);
            Node = null;
        }
    }

    public override void Redo(WorkspaceViewModel workspaceVM)
    {
        if (Node is not null)
        {
            workspaceVM.Nodes.Remove(Node);
        }
        if (NodeBeforeHistoryAction is not null)
        {
            workspaceVM.Nodes.Add(NodeBeforeHistoryAction);
            Node = NodeBeforeHistoryAction;
            NodeBeforeHistoryAction = null;
        }
    }
}