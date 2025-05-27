using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaEdit;
using CommunityToolkit.Mvvm.Messaging;
using dboard.Messages;
using dboard.Models;
using dboard.Models.Actions.TextEdit;

namespace dboard.Views;

public partial class TextEditView : Border
{

    TextEditor? _textEditor;
    string _previousText = "";

    public TextEditView()
    {
        InitializeComponent();
        _textEditor = this.FindControl<TextEditor>("TextEdit");
        NotesModel notesModel = (NotesModel)DataContext;
        if (notesModel is not null)
        {
            _previousText = notesModel.Text;
        }
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

    protected void OnTextEditorLostFocus(object sender, RoutedEventArgs e)
    {
        NotesModel notesModel = (NotesModel)DataContext;
        if (notesModel is not null && notesModel.Text != _previousText)
        {
                string currentText = notesModel.Text;
                notesModel.Text = _previousText;
                WeakReferenceMessenger.Default.Send(new LogActionMessage(new OpenTextEditActionModel(notesModel)));
                notesModel.Text = currentText;
                _previousText = currentText;
        }
    }
}