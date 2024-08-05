using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Messages;
using mystery_app.Models;

namespace mystery_app.ViewModels;

public abstract partial class NodeViewModelBase : ObservableObject
{
    public abstract NodeModelBase NodeBase { get; set; }

    [ObservableProperty]
    private bool _isSelected;

    [ObservableProperty]
    private bool _isEdit;

    [RelayCommand]
    private void DeleteNode()
    {
        WeakReferenceMessenger.Default.Send(new DeleteNodeMessage(this));
    }

    [RelayCommand]
    private void CopyNode()
    {
        WeakReferenceMessenger.Default.Send(new CopyNodeMessage(this));
    }

    public abstract NodeViewModelBase Clone();
}