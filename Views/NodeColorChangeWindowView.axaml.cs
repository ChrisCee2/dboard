using Avalonia.Controls;
using dboard.Tools;
using dboard.ViewModels;

namespace dboard.Views;

public partial class NodeColorChangeWindowView : Window
{
    public NodeColorChangeWindowView(NodeColorChangeViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
        Closing += OnWindowClosing;
    }

    public void OnWindowClosing(object sender, WindowClosingEventArgs e)
    {
        NodeChangeColorTool.ColorViewClosed();
    }
}