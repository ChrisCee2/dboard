using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using dboard.Constants;
using dboard.Messages;
using dboard.Models;

namespace dboard.ViewModels;

public partial class NotesViewModel : ObservableObject
{
    [ObservableProperty]
    public Collection<NoteViewModel> _notes;
    [ObservableProperty]
    private SettingsModel _sharedSettings;

    public NotesViewModel(SettingsModel sharedSettings)
    {
        SharedSettings = sharedSettings;
        Notes = new ObservableCollection<NoteViewModel>();
    }

    public NotesViewModel(SettingsModel sharedSettings, List<NoteViewModel> noteViewModels)
    {
        SharedSettings = sharedSettings;
        Notes = new ObservableCollection<NoteViewModel>(noteViewModels);
    }

    public NotesViewModel(SettingsModel sharedSettings, List<NoteModel> noteModels)
    {
        SharedSettings = sharedSettings;
        Notes = new ObservableCollection<NoteViewModel>();
        foreach (NoteModel noteModel in noteModels)
        {
            Notes.Add(new NoteViewModel(noteModel));
        }
    }

    public void CreateNote()
    {
        Color noteColor = new Color(
            SharedSettings.ModeModel.AccentA,
            SharedSettings.ModeModel.AccentR,
            SharedSettings.ModeModel.AccentG,
            SharedSettings.ModeModel.AccentB
        );
        Notes.Add(new NoteViewModel(new NoteModel(noteColor)));
    }

    [RelayCommand]
    private void DeleteNote(NoteViewModel noteViewModel)
    {
        Notes.Remove(noteViewModel);
    }
}
