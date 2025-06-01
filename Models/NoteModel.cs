using CommunityToolkit.Mvvm.ComponentModel;
using dboard.Constants;

namespace dboard.Models;

public partial class NoteModel : ObservableObject
{
    public NoteModel() {}

    public NoteModel(string text, double paneLength)
    {
        Text = text;
        PaneLength = paneLength;
    }

    public void Copy(NoteModel notesModelToCopy)
    {
        Text = notesModelToCopy.Text;
        PaneLength = notesModelToCopy.PaneLength;
    }

    public NoteModel Clone()
    {
        return new NoteModel(Text, PaneLength);
    }

    [ObservableProperty]
    private string _title = "";
    [ObservableProperty]
    private string _text;
    [ObservableProperty]
    private double _paneLength = ToolbarConstants.NOTES_PANE_DEFAULT_LEN;
}
