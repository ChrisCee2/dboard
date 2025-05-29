using Avalonia;
using Avalonia.Controls;
using dboard.ViewModels;

namespace dboard.Views;

public partial class MainWindowView : Window
{
    public MainWindowView()
    {
        InitializeComponent();
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
            e.Cancel = true;
            Window dialogWindow = new SaveDialogWindowView();

            var centerX = Position.X + (int)((ClientSize.Width - dialogWindow.Width) / 2);
            var centerY = Position.Y + (int)((ClientSize.Height - dialogWindow.Height) / 2);
            dialogWindow.Position = new PixelPoint(centerX, centerY);

            await dialogWindow.ShowDialog(this);
        }
    }
}