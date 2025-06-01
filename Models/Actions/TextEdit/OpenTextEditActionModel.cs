using dboard.ViewModels;

namespace dboard.Models.Actions.TextEdit;


public partial class OpenTextEditActionModel : TextEditActionModelBase
{
    NoteModel? _textEditBeforeEdit;
    NoteModel? _textEditAfterEdit;

    public OpenTextEditActionModel(
        NoteModel textEdit
    ) : base(textEdit) { _textEditBeforeEdit = textEdit.Clone(); }

    public override void Undo(WorkspaceViewModel workspaceVM)
    {
        if (_textEditBeforeEdit != null)
        {
            _textEditAfterEdit = TextEdit.Clone();
            TextEdit.Copy(_textEditBeforeEdit);
        }
        _textEditBeforeEdit = null;
    }

    public override void Redo(WorkspaceViewModel workspaceVM)
    {
        if (_textEditAfterEdit != null)
        {
            _textEditBeforeEdit = TextEdit.Clone();
            TextEdit.Copy(_textEditAfterEdit);
        }
        _textEditAfterEdit = null;
    }
}