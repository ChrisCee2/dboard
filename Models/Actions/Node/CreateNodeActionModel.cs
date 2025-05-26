using dboard.ViewModels;

namespace dboard.Models.Actions.Node;


public partial class CreateNodeActionModel : NodeActionModelBase
{
    public CreateNodeActionModel(
        NodeViewModelBase node
    ) : base(node) { }

    public override void Undo(WorkspaceViewModel workspaceVM)
    {
        workspaceVM.Nodes.Remove(Node);
    }

    public override void Redo(WorkspaceViewModel workspaceVM)
    {
        if (!workspaceVM.Nodes.Contains(Node))
        {
            workspaceVM.Nodes.Add(Node);
        }
    }
}