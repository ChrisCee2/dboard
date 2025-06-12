using Avalonia.Controls;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Messaging;
using dboard.Messages;

namespace dboard.Views;

public partial class NoteTextView : Border
{
    public NoteTextView()
    {
        InitializeComponent();
    }

    protected async void Collapse(object sender, RoutedEventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new SelectNoteMessage(null));
    }
}