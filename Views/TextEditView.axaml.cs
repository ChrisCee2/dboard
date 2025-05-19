using System;
using Avalonia.Controls;
using AvaloniaEdit;
using dboard.Models;

namespace dboard.Views;

public partial class TextEditView : Border
{

    TextEditor? _textEditor;

    public TextEditView()
    {
        InitializeComponent();
        _textEditor = this.FindControl<TextEditor>("TextEdit");
        NotesModel notesModel = (NotesModel)DataContext;
    }

    private void TextChanged(object sender, EventArgs eventArgs)
    {
        if (_textEditor != null && _textEditor.Document != null)
        {
            NotesModel notesModel = (NotesModel)DataContext;
            if (notesModel != null)
            {
                var caretOffset = _textEditor.CaretOffset;
                _textEditor.CaretOffset = caretOffset;
                notesModel.Text = _textEditor.Text;
                _textEditor.CaretOffset = caretOffset;
            }
        }
    }
}