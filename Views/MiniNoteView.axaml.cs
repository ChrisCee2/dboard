using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Messaging;
using dboard.Messages;
using dboard.ViewModels;

namespace dboard.Views;

public partial class MiniNoteView : Border
{
    public MiniNoteView()
    {
        InitializeComponent();
    }

    protected async void Expand(object sender, RoutedEventArgs e)
    {
        if (DataContext is NoteViewModel viewModel)
        {
            WeakReferenceMessenger.Default.Send(new SelectNoteMessage(viewModel));
        }
    }
}