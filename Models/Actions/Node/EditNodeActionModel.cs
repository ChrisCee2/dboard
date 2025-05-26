using dboard.ViewModels;

namespace dboard.Models.Actions.Node;


public partial class EditNodeActionModel : NodeActionModelBase
{
    NodeViewModelBase? _nodeBeforeEdit;
    NodeViewModelBase? _nodeAfterEdit;

    public EditNodeActionModel(
        NodeViewModelBase node
    ) : base(node) { _nodeBeforeEdit = node.Clone(); }

    public override void Undo(WorkspaceViewModel workspaceVM)
    {
        if (_nodeBeforeEdit != null)
        {
            _nodeAfterEdit = Node.Clone();
            Node.Copy(_nodeBeforeEdit);
        }
        _nodeBeforeEdit = null;
    }

    public override void Redo(WorkspaceViewModel workspaceVM)
    {
        if (_nodeAfterEdit != null)
        {
            _nodeBeforeEdit = Node.Clone();
            Node.Copy(_nodeAfterEdit);
        }
        _nodeAfterEdit = null;
    }
}