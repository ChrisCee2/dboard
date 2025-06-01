using CommunityToolkit.Mvvm.ComponentModel;
using dboard.ViewModels;

namespace dboard.Models.Actions.TextEdit;


public abstract partial class TextEditActionModelBase : ActionModelBase
{
    [ObservableProperty]
    private TextEditViewModel _textEdit;

    public TextEditActionModelBase(TextEditViewModel textEdit)
    {
        TextEdit = textEdit;
    }
}