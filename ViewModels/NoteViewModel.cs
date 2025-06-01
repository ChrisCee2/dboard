using CommunityToolkit.Mvvm.ComponentModel;
using dboard.Models;

namespace dboard.ViewModels;

public partial class NoteViewModel : ObservableObject
{
    [ObservableProperty]
    public NoteModel _notes;

    public NoteViewModel(NoteModel notes)
    {
        Notes = notes;
    }

    public TextEditViewModel Clone()
    {
        return new TextEditViewModel(Notes.Clone());
    }

    public void Copy(TextEditViewModel textEditToCopy)
    {
        Notes.Copy(textEditToCopy.Notes);
    }
}
