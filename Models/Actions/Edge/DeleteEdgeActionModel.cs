using dboard.ViewModels;

namespace dboard.Models;


public partial class DeleteEdgeActionModel : EdgeActionModelBase
{
    public DeleteEdgeActionModel(
        EdgeViewModel edge
    ) : base(edge) { }

    public override void Undo(WorkspaceViewModel workspaceVM)
    {
        if (!workspaceVM.Edges.Contains(Edge))
        {
            workspaceVM.Edges.Add(Edge);
        }
    }

    public override void Redo(WorkspaceViewModel workspaceVM)
    {
        workspaceVM.Edges.Remove(Edge);
    }
}