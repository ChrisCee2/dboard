using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace dboard.ViewModels;

public partial class NodeColorChangeViewModel : ObservableObject
{
    [ObservableProperty]
    private NodeViewModelBase _viewModel;
    [ObservableProperty]
    private Color _nodeColor;

    public NodeColorChangeViewModel(NodeViewModelBase nodeViewModel)
    {
        ViewModel = nodeViewModel;
        NodeColor = new Color(
            ViewModel.NodeBase.A,
            ViewModel.NodeBase.R,
            ViewModel.NodeBase.G,
            ViewModel.NodeBase.B);
    }

    partial void OnNodeColorChanged(Color value)
    {
        ViewModel.NodeBase.A = value.A;
        ViewModel.NodeBase.R = value.R;
        ViewModel.NodeBase.G = value.G;
        ViewModel.NodeBase.B = value.B;
    }
}