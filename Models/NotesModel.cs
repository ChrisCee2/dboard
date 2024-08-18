namespace mystery_app.Models;

public class NotesModel
{
    public NotesModel() {}

    public NotesModel(string text)
    {
        Text = text;
    }

    private string _text;

    public string Text
    {
        get { return _text; }
        set { _text = value; }
    }
}
