using CommunityToolkit.Mvvm.ComponentModel;
using dboard.ViewModels;

namespace dboard.Models.Actions.TextEdit;


public abstract partial class TextEditActionModelBase : ActionModelBase
{
    [ObservableProperty]
    private NotesModel _textEdit;

    public TextEditActionModelBase(NotesModel textEdit)
    {
        TextEdit = textEdit;
    }
}