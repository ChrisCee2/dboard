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
        Closing += ((MainWindowViewModel)DataContext).OnWindowClosing;
    }
}