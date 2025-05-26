using CommunityToolkit.Mvvm.ComponentModel;
using dboard.ViewModels;

namespace dboard.Models;


public abstract partial class ActionModelBase : ObservableObject
{
    abstract public void Undo(WorkspaceViewModel workspaceVM);

    abstract public void Redo(WorkspaceViewModel workspaceVM);
}
