using System.Collections.Generic;
using dboard.ViewModels;

namespace dboard.Models.Actions.Node;


public partial class MultiActionModel : ActionModelBase
{
    List<ActionModelBase> _actions; // Should be ordered from first to last action
    public MultiActionModel(
        List<ActionModelBase> actions
    ) { _actions = actions; }

    public override void Undo(WorkspaceViewModel workspaceVM)
    {
        for (int i = _actions.Count - 1; i >= 0; i--)
        {
            _actions[i].Undo(workspaceVM);
        }
    }

    public override void Redo(WorkspaceViewModel workspaceVM)
    {
        foreach (var action in _actions)
        {
            action.Redo(workspaceVM);
        }
    }
}