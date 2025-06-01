using CommunityToolkit.Mvvm.ComponentModel;
using dboard.Models;

namespace dboard.ViewModels;

public partial class NoteViewModel : ObservableObject
{
    [ObservableProperty]
    public NoteModel _note;

    public NoteViewModel(NoteModel note)
    {
        Note = note;
    }

    public TextEditViewModel Clone()
    {
        return new TextEditViewModel(Note.Clone());
    }

    public void Copy(TextEditViewModel textEditToCopy)
    {
        Note.Copy(textEditToCopy.Note);
    }
}
