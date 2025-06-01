using CommunityToolkit.Mvvm.ComponentModel;
using dboard.ViewModels;

namespace dboard.Models.Actions.TextEdit;


public abstract partial class TextEditActionModelBase : ActionModelBase
{
    [ObservableProperty]
    private NoteModel _textEdit;

    public TextEditActionModelBase(NoteModel textEdit)
    {
        TextEdit = textEdit;
    }
}