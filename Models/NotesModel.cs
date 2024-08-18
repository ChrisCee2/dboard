using CommunityToolkit.Mvvm.ComponentModel;
using mystery_app.Constants;

namespace mystery_app.Models;

public partial class NotesModel : ObservableObject
{
    public NotesModel() {}

    public NotesModel(string text, double paneLength)
    {
        Text = text;
        PaneLength = paneLength;
    }

    [ObservableProperty]
    private string _text;
    [ObservableProperty]
    private double _paneLength = ToolbarConstants.NOTES_PANE_DEFAULT_LEN;
}
