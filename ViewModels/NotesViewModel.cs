using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using dboard.Models;

namespace dboard.ViewModels;

public partial class NotesViewModel : ObservableObject
{
    [ObservableProperty]
    public Collection<NoteModel> _notes = new Collection<NoteModel>();

    public NotesViewModel()
    {
    }
}
