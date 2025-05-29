using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Logging;
using CommunityToolkit.Mvvm.Messaging;
using dboard.Messages;
using dboard.ViewModels;
using Tmds.DBus.Protocol;

namespace dboard.Views;

public partial class SaveDialogWindowView : Window
{
    public SaveDialogWindowView(SaveDialogWindowViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }

    // Functions with messages for each of the buttons
    protected void Save(object sender, RoutedEventArgs args)
    {
        WeakReferenceMessenger.Default.Send(new SaveDialogSaveMessage(this));
    }

    protected void Continue(object sender, RoutedEventArgs args)
    {
        Close(true);
    }

    protected void Cancel(object sender, RoutedEventArgs args)
    {
        Close(false);
    }
}