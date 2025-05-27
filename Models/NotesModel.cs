using CommunityToolkit.Mvvm.ComponentModel;
using dboard.Constants;

namespace dboard.Models;

public partial class NotesModel : ObservableObject
{
    public NotesModel() {}

    public NotesModel(string text, double paneLength)
    {
        Text = text;
        PaneLength = paneLength;
    }

    public void Copy(NotesModel notesModelToCopy)
    {
        Text = notesModelToCopy.Text;
        PaneLength = notesModelToCopy.PaneLength;
    }

    public NotesModel Clone()
    {
        return new NotesModel(Text, PaneLength);
    }

    [ObservableProperty]
    private string _text;
    [ObservableProperty]
    private double _paneLength = ToolbarConstants.NOTES_PANE_DEFAULT_LEN;
}
