using CommunityToolkit.Mvvm.ComponentModel;
using dboard.Models;

namespace dboard.ViewModels;

public partial class TextEditViewModel : ObservableObject
{
    [ObservableProperty]
    public NotesModel _notes;

    public TextEditViewModel(NotesModel notes)
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
