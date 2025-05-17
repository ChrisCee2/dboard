using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Logging;
using Avalonia.Styling;
using dboard.ViewModels;

namespace dboard.Views;

public partial class TextEditView : Panel
{

    public TextEditView()
    {
        InitializeComponent();
    }

    public void ShowTextBox(object sender, PointerReleasedEventArgs e)
    {
        TextBox textBox = this.FindControl<TextBox>("NotesTextBox");
        SelectableTextBlock textBlock = this.FindControl<SelectableTextBlock>("NotesTextBlock");
        int selectionStart = textBlock.SelectionStart;
        int selectionEnd = textBlock.SelectionEnd;
        textBlock.IsVisible = false;
        textBox.IsVisible = true;
        textBox.Focus();
        textBox.SelectionStart = selectionStart;
        textBox.SelectionEnd = selectionEnd;
        TextEditViewModel context = (TextEditViewModel)DataContext;
    }

    public void HideTextBox(object sender, RoutedEventArgs e)
    {
        TextBox textBox = this.FindControl<TextBox>("NotesTextBox");
        SelectableTextBlock textBlock = this.FindControl<SelectableTextBlock>("NotesTextBlock");
        textBox.IsVisible = false;
        textBlock.IsVisible = true;
        TextEditViewModel context = (TextEditViewModel)DataContext;
    }
}