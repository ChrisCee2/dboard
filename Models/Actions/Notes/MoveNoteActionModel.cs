using dboard.ViewModels;

namespace dboard.Models.Actions.Notes;


public partial class MoveNoteActionModel : NoteActionModelBase
{
    int _oldIndex;
    int _newIndex;

    public MoveNoteActionModel(
        int oldIndex,
        int newIndex,
        NoteViewModel note
    ) : base(note) 
    {
        _oldIndex = oldIndex;
        _newIndex = newIndex;
    }

    public override void Undo(WorkspaceViewModel workspaceVM)
    {
        workspaceVM.Notes.SetNoteIndex(Note, _oldIndex);
    }

    public override void Redo(WorkspaceViewModel workspaceVM)
    {
        workspaceVM.Notes.SetNoteIndex(Note, _newIndex);
    }
}