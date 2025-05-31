using Avalonia;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using dboard.Messages;
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

            e.Cancel = true;
            Window dialogWindow = new SaveDialogWindowView(new SaveDialogWindowViewModel(viewModel.SharedSettings));

            var centerX = Position.X + (int)((ClientSize.Width - dialogWindow.Width) / 2);
            var centerY = Position.Y + (int)((ClientSize.Height - dialogWindow.Height) / 2);
            dialogWindow.Position = new PixelPoint(centerX, centerY);

            viewModel.ShouldClose = await dialogWindow.ShowDialog<bool>(this);
        }
    }
}