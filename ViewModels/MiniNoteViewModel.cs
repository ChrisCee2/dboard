using CommunityToolkit.Mvvm.ComponentModel;

namespace dboard.ViewModels;

public partial class MiniNoteViewModel : ObservableObject
{
    [ObservableProperty]
    public TextEditViewModel _title;
    [ObservableProperty]
    public TextEditViewModel _text;

    public MiniNoteViewModel(TextEditViewModel title, TextEditViewModel text)
    {
        Title = title;
        Text = text;
    }

    public MiniNoteViewModel Clone()
    {
        return new MiniNoteViewModel(Title.Clone(), Text.Clone());
    }

    public void Copy(MiniNoteViewModel miniNoteToCopy)
    {
        Text = miniNoteToCopy.Text;
    }
}
