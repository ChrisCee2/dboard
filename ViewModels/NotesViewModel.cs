using CommunityToolkit.Mvvm.ComponentModel;
using mystery_app.Models;

namespace mystery_app.ViewModels;

public partial class NotesViewModel : ObservableObject
{
    public NotesViewModel()
    {
        Notes = new NotesModel();
    }

    public NotesViewModel(NotesModel notes)
    {
        Notes = notes;
    }

    [ObservableProperty]
    private NotesModel _notes;
}