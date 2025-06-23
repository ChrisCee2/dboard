using Avalonia.Controls;
using dboard.ViewModels;

namespace dboard.Views;

public partial class NodeColorChangeView : UserControl
{
    public NodeColorChangeView()
    {
        InitializeComponent();
    }

    public NodeColorChangeView(NodeColorChangeViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}