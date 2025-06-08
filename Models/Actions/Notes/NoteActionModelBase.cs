using CommunityToolkit.Mvvm.ComponentModel;
using dboard.ViewModels;

namespace dboard.Models.Actions.Notes;


public abstract partial class NoteActionModelBase : ActionModelBase
{
    [ObservableProperty]
    private NoteViewModel _note;

    public NoteActionModelBase(NoteViewModel note)
    {
        Note = note;
    }
}
