using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using dboard.Constants;

namespace dboard.Models;

public partial class NoteModel : ObservableObject
{
    public NoteModel() {}

    public NoteModel(Color color)
    {
        A = color.A;
        R = color.R;
        G = color.G;
        B = color.B;
    }

    public NoteModel(string text, double paneLength, Color color)
    {
        Text = text;
        PaneLength = paneLength;
        A = color.A;
        R = color.R;
        G = color.G;
        B = color.B;
    }

    public void Copy(NoteModel notesModelToCopy)
    {
        Text = notesModelToCopy.Text;
        PaneLength = notesModelToCopy.PaneLength;
        A = notesModelToCopy.A;
        R = notesModelToCopy.R;
        G = notesModelToCopy.G;
        B = notesModelToCopy.B;
    }

    public NoteModel Clone()
    {
        return new NoteModel(Text, PaneLength, new Color(A, R, G, B));
    }

    [ObservableProperty]
    private string _title = "";
    [ObservableProperty]
    private string _text;
    [ObservableProperty]
    private double _paneLength = ToolbarConstants.NOTES_PANE_DEFAULT_LEN;

    [ObservableProperty]
    private byte _a;
    [ObservableProperty]
    private byte _r;
    [ObservableProperty]
    private byte _g;
    [ObservableProperty]
    private byte _b;
}
