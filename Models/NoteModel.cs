using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;

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

    public NoteModel(string text, Color color)
    {
        Text = text;
        A = color.A;
        R = color.R;
        G = color.G;
        B = color.B;
    }

    public void Copy(NoteModel notesModelToCopy)
    {
        Text = notesModelToCopy.Text;
        A = notesModelToCopy.A;
        R = notesModelToCopy.R;
        G = notesModelToCopy.G;
        B = notesModelToCopy.B;
    }

    public NoteModel Clone()
    {
        return new NoteModel(Text, new Color(A, R, G, B));
    }

    [ObservableProperty]
    private string _title = "";
    [ObservableProperty]
    private string _text;

    [ObservableProperty]
    private byte _a;
    [ObservableProperty]
    private byte _r;
    [ObservableProperty]
    private byte _g;
    [ObservableProperty]
    private byte _b;
}
