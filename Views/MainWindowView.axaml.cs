using Avalonia;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using dboard.Messages;
using dboard.Tools;
using dboard.ViewModels;

namespace dboard.Views;

public partial class MainWindowView : Window
{
    public MainWindowView()
    {
        InitializeComponent();
        WeakReferenceMessenger.Default.Register<CloseAppMessage>(this, (sender, message) =>
        {
            Close();
        });
    }

    protected override void OnDataContextEndUpdate()
    {
        base.OnDataContextEndUpdate();
        Closing += OnWindowClosing;
    }

    public async void OnWindowClosing(object sender, WindowClosingEventArgs e)
    {
        MainWindowViewModel viewModel = (MainWindowViewModel)DataContext;
        if (viewModel != null)
        {
            viewModel.SaveSettings();
            if (viewModel.ShouldClose == true || !viewModel.ShouldShowSaveDialog())
            {
                return;
            }
            else if (SaveDialogTool.CanShowSaveDialog())
            {
                e.Cancel = true;

                viewModel.ShouldClose = await SaveDialogTool.ShowSaveDialog(this, viewModel.SharedSettings);
            }
            e.Cancel = true;
        }
    }
}