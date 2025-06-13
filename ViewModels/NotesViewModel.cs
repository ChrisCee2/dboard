using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using dboard.Constants;
using dboard.Messages;
using dboard.Models;
using dboard.Models.Actions.Notes;

namespace dboard.ViewModels;

public partial class NotesViewModel : ObservableObject
{
    [ObservableProperty]
    public Collection<NoteViewModel> _notes;
    [ObservableProperty]
    private NoteViewModel? _selectedNote = null;
    [ObservableProperty]
    private SettingsModel _sharedSettings;

    // For note moving
    [ObservableProperty]
    private NoteViewModel? _noteToMove = null;
    [ObservableProperty]
    private NoteViewModel? _lastNoteCursorWasOver = null;
    [ObservableProperty]
    private bool _cursorIsAboveCurrentNote = false;

    public void SetUpMessengers()
    {
        WeakReferenceMessenger.Default.Register<SelectNoteMessage>(this, (sender, message) =>
        {
            SelectedNote = message.Value;
        });
    }

    public NotesViewModel(SettingsModel sharedSettings)
    {
        SharedSettings = sharedSettings;
        Notes = new ObservableCollection<NoteViewModel>();
        SetUpMessengers();
    }

    public NotesViewModel(SettingsModel sharedSettings, List<NoteViewModel> noteViewModels)
    {
        SharedSettings = sharedSettings;
        Notes = new ObservableCollection<NoteViewModel>(noteViewModels);
        SetUpMessengers();
    }

    public NotesViewModel(SettingsModel sharedSettings, List<NoteModel> noteModels)
    {
        SharedSettings = sharedSettings;
        Notes = new ObservableCollection<NoteViewModel>();
        foreach (NoteModel noteModel in noteModels)
        {
            Notes.Add(new NoteViewModel(noteModel));
        }
        SetUpMessengers();
    }

    public void CreateNote()
    {
        Color noteColor = new Color(
            SharedSettings.ModeModel.AccentA,
            SharedSettings.ModeModel.AccentR,
            SharedSettings.ModeModel.AccentG,
            SharedSettings.ModeModel.AccentB
        );
        NoteViewModel noteViewModel = new NoteViewModel(new NoteModel(noteColor));
        Notes.Add(noteViewModel);
        int index = Notes.IndexOf(noteViewModel);
        WeakReferenceMessenger.Default.Send(new LogActionMessage(new CreateNoteActionModel(index, noteViewModel)));
    }

    [RelayCommand]
    private void DeleteNote(NoteViewModel noteViewModel)
    {
        if (SelectedNote == noteViewModel)
        {
            SelectedNote = null;
        }
        var index = Notes.IndexOf(noteViewModel);
        Notes.RemoveAt(index);
        WeakReferenceMessenger.Default.Send(new LogActionMessage(new DeleteNoteActionModel(index, noteViewModel)));
    }
}
