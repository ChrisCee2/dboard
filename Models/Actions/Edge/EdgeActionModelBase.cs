using CommunityToolkit.Mvvm.ComponentModel;
using dboard.ViewModels;

namespace dboard.Models;


public abstract partial class EdgeActionModelBase : ActionModelBase
{
    [ObservableProperty]
    private EdgeViewModel _edge;

    public EdgeActionModelBase(EdgeViewModel edge)
    {
        Edge = edge;
    }
}
