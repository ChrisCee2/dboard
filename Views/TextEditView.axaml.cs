using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaEdit;
using CommunityToolkit.Mvvm.Messaging;
using dboard.Messages;
using dboard.Models;
using dboard.Models.Actions.TextEdit;
using dboard.ViewModels;

namespace dboard.Views;

public partial class TextEditView : Border
{

    TextEditor? _textEditor;
    string _previousText = "";

    public TextEditView()
    {
        InitializeComponent();
        _textEditor = this.FindControl<TextEditor>("TextEdit");
        TextEditViewModel textEditViewModel = (TextEditViewModel)DataContext;
        if (textEditViewModel is not null)
        {
            _previousText = textEditViewModel.Text;
        }
    }

    private void TextChanged(object sender, EventArgs eventArgs)
    {
        if (_textEditor != null && _textEditor.Document != null)
        {
            TextEditViewModel textEditViewModel = (TextEditViewModel)DataContext;
            if (textEditViewModel != null)
            {
                var caretOffset = _textEditor.CaretOffset;
                _textEditor.CaretOffset = caretOffset;
                textEditViewModel.Text = _textEditor.Text;
                _textEditor.CaretOffset = caretOffset;
            }
        }
    }

    protected void OnTextEditorLostFocus(object sender, RoutedEventArgs e)
    {
        TextEditViewModel textEditViewModel = (TextEditViewModel)DataContext;
        if (textEditViewModel is not null && textEditViewModel.Text != _previousText)
        {
                string currentText = textEditViewModel.Text;
                textEditViewModel.Text = _previousText;
                WeakReferenceMessenger.Default.Send(new LogActionMessage(new OpenTextEditActionModel(textEditViewModel)));
                textEditViewModel.Text = currentText;
                _previousText = currentText;
        }
    }
}