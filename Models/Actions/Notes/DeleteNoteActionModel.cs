using dboard.ViewModels;

namespace dboard.Models.Actions.Notes;


public partial class DeleteNoteActionModel : NoteActionModelBase
{

    int _index;

    public DeleteNoteActionModel(
        int index,
        NoteViewModel note
    ) : base(note) { _index = index; }

    public override void Undo(WorkspaceViewModel workspaceVM)
    {
        if (!workspaceVM.Notes.Notes.Contains(Note))
        {
            workspaceVM.Notes.Notes.Insert(_index, Note);
        }
    }

    public override void Redo(WorkspaceViewModel workspaceVM)
    {
        workspaceVM.Notes.Notes.RemoveAt(_index);
    }
}