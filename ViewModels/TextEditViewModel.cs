using CommunityToolkit.Mvvm.ComponentModel;
using dboard.Models;
using DynamicData;

namespace dboard.ViewModels;

public partial class TextEditViewModel : ObservableObject
{

    public TextEditViewModel(NotesModel notes)
    {
        Notes = notes;
    }

    [ObservableProperty]
    public NotesModel _notes;
    [ObservableProperty]
    public bool _isEditing = false;
}
