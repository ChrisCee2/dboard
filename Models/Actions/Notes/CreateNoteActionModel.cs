using dboard.ViewModels;

namespace dboard.Models.Actions.Notes;


public partial class CreateNoteActionModel : NoteActionModelBase
{
    int _index;

    public CreateNoteActionModel(
        int index,
        NoteViewModel note
    ) : base(note) { _index = index; }

    public override void Undo(WorkspaceViewModel workspaceVM)
    {
        workspaceVM.Notes.Notes.RemoveAt(_index);
    }

    public override void Redo(WorkspaceViewModel workspaceVM)
    {
        if (!workspaceVM.Notes.Notes.Contains(Note))
        {
            workspaceVM.Notes.Notes.Insert(_index, Note);
        }
    }
}