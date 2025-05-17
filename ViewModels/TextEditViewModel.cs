using CommunityToolkit.Mvvm.ComponentModel;
using dboard.Models;

namespace dboard.ViewModels;

public partial class TextEditViewModel : ObservableObject
{

    public TextEditViewModel(NotesModel notes)
    {
        Notes = notes;
    }

    [ObservableProperty]
    public NotesModel _notes;
}
