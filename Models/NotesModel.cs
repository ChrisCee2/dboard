namespace mystery_app.Models;

public class NotesModel
{
    public NotesModel() {}

    public NotesModel(string notes)
    {
        Notes = notes;
    }

    private string _notes;

    public string Notes
    {
        get { return _notes; }
        set { _notes = value; }
    }
}
