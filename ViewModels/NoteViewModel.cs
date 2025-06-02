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

    public NoteViewModel(NoteModel note)
    {
        Note = note;
        Title = new TextEditViewModel(note.Title);
        Text = new TextEditViewModel(note.Text);
    }

    public NoteViewModel Clone()
    {
        return new NoteViewModel(Note.Clone());
    }

    public void Copy(NoteViewModel noteToCopy)
    {
        Title = noteToCopy.Title;
        Text = noteToCopy.Text;
    }

    partial void OnTitleChanged(TextEditViewModel value)
    {
        Note.Title = value.Text;
    }

    partial void OnTextChanged(TextEditViewModel value)
    {
        Note.Text = value.Text;
    }
}
