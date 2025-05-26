using dboard.ViewModels;

namespace dboard.Models;


public partial class CreateEdgeActionModel : EdgeActionModelBase
{
    public CreateEdgeActionModel(
        EdgeViewModel edge
    ) : base(edge) { }

    public override void Undo(WorkspaceViewModel workspaceVM)
    {
        workspaceVM.Edges.Remove(Edge);
    }

    public override void Redo(WorkspaceViewModel workspaceVM)
    {
        if (!workspaceVM.Edges.Contains(Edge))
        {
            workspaceVM.Edges.Add(Edge);
        }
    }
}