using Avalonia.Controls.Primitives;
using Avalonia.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using dboard.Models;

namespace dboard.ViewModels;

public partial class NoteViewModel : ObservableObject
{
    [ObservableProperty]
    public NoteModel _note;
    [ObservableProperty]
    public TextEditViewModel _title;
    [ObservableProperty]
    public TextEditViewModel _text;

    public NoteViewModel()
    {
        Note = new NoteModel();
        Title = new TextEditViewModel(Note.Title, false);
        Text = new TextEditViewModel(Note.Text);
    }

    public NoteViewModel(NoteModel note)
    {
        Note = note;
        Title = new TextEditViewModel(note.Title, false);
        Text = new TextEditViewModel(note.Text);
    }

    public NoteViewModel Clone()
    {
        return new NoteViewModel(Note.Clone());
    }

    public void Copy(NoteViewModel noteToCopy)
    {
        Title = noteToCopy.Title.Clone();
        Text = noteToCopy.Text.Clone();
    }

    // Jank way to handle updating note model, this is done when saving the notes
    public void UpdateNoteModel()
    {
        Note.Title = Title.Text;
        Note.Text = Text.Text;
    }
}
