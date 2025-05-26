using dboard.ViewModels;

namespace dboard.Models.Actions.Node;


public partial class DeleteNodeActionModel : NodeActionModelBase
{
    public DeleteNodeActionModel(
        NodeViewModelBase node
    ) : base(node) { }

    public override void Undo(WorkspaceViewModel workspaceVM)
    {
        if (!workspaceVM.Nodes.Contains(Node))
        {
            workspaceVM.Nodes.Add(Node);
        }
    }

    public override void Redo(WorkspaceViewModel workspaceVM)
    {
        workspaceVM.Nodes.Remove(Node);
    }
}