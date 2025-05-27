using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using dboard.Messages;
using dboard.Models;

namespace dboard.ViewModels;

public partial class EdgeViewModel : ObservableObject
{
    public EdgeViewModel(EdgeModel edgeModel)
    {
        Edge = edgeModel;
    }

    [ObservableProperty]
    private EdgeModel _edge;
    [ObservableProperty]
    private bool _isSelected;
    public bool DescIsNull() => Edge.Description is null;
    public bool DescIsNotNull() => Edge.Description is not null;

    [RelayCommand]
    private void Delete()
    {
        WeakReferenceMessenger.Default.Send(new DeleteMessage(""));
    }

    [RelayCommand(CanExecute = nameof(DescIsNull))]
    private void AddDesc()
    {
        // TODO: Can optimize this by just storing the description and reference to this
        WeakReferenceMessenger.Default.Send(new LogActionMessage(new EditEdgeActionModel(this)));
        Edge.Description = "";
    }

    [RelayCommand(CanExecute = nameof(DescIsNotNull))]
    private void RemoveDesc()
    {
        // TODO: Can optimize this by just storing the description and reference to this
        WeakReferenceMessenger.Default.Send(new LogActionMessage(new EditEdgeActionModel(this)));
        Edge.Description = null;
    }

    public EdgeViewModel Clone()
    {
        return new EdgeViewModel(new EdgeModel(
            Edge.FromNode,
            Edge.ToNode,
            Edge.Description,
            Edge.A,
            Edge.R,
            Edge.G,
            Edge.B,
            Edge.Thickness));
    }

    public EdgeViewModel CloneWithNewNodes(NodeModelBase fromNode, NodeModelBase toNode)
    {
        return new EdgeViewModel(new EdgeModel(
            fromNode,
            toNode,
            Edge.Description,
            Edge.A,
            Edge.R,
            Edge.G,
            Edge.B,
            Edge.Thickness));
    }

    public void Copy(EdgeViewModel edgeToCopy)
    {
        Edge.Copy(edgeToCopy.Edge);
    }
}