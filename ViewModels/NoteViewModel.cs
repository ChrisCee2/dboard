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

    public NoteViewModel Clone()
    {
        return new NoteViewModel(Note.Clone());
    }

    public void Copy(NoteViewModel textEditToCopy)
    {
        Note.Copy(textEditToCopy.Note);
    }
}
