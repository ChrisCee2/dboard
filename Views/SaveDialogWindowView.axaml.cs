using Avalonia.Controls;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Messaging;
using dboard.Messages;
using dboard.Tools;
using dboard.ViewModels;

namespace dboard.Views;

public partial class SaveDialogWindowView : Window
{
    public SaveDialogWindowView(SaveDialogWindowViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();

        Closing += OnWindowClosing;
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

    public void OnWindowClosing(object sender, WindowClosingEventArgs e)
    {
        SaveDialogTool.SaveDialogClosed();
    }
}