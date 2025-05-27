using dboard.ViewModels;

namespace dboard.Models;


public partial class EditEdgeActionModel : EdgeActionModelBase
{
    EdgeViewModel? _edgeBeforeEdit;
    EdgeViewModel? _edgeAfterEdit;

    public EditEdgeActionModel(
        EdgeViewModel edge
    ) : base(edge) { _edgeBeforeEdit = edge.Clone(); }

    public override void Undo(WorkspaceViewModel workspaceVM)
    {
        if (_edgeBeforeEdit != null)
        {
            _edgeAfterEdit = Edge.Clone();
            Edge.Copy(_edgeBeforeEdit);
        }
        _edgeBeforeEdit = null;
    }

    public override void Redo(WorkspaceViewModel workspaceVM)
    {
        if (_edgeAfterEdit != null)
        {
            _edgeBeforeEdit = Edge.Clone();
            Edge.Copy(_edgeAfterEdit);
        }
        _edgeAfterEdit = null;
    }
}