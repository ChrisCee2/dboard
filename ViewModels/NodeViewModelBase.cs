using System;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using dboard.Messages;
using dboard.Models;

namespace dboard.ViewModels;

public abstract partial class NodeViewModelBase : ObservableObject
{
    public abstract NodeModelBase NodeBase { get; set; }

    [ObservableProperty]
    private bool _initialized;

    [ObservableProperty]
    private bool _isSelected;

    [ObservableProperty]
    private bool _isEdit;

    [ObservableProperty]
    private Control control;

    public bool NodeBaseHasImagePathField() => NodeBase.GetType().GetProperty("ImagePath") != null;
    public bool NodeBaseHasImage() => NodeBase.GetType().GetProperty("ImagePath") != null && NodeBase.GetType().GetProperty("ImagePath").GetValue(NodeBase) != null;

    [RelayCommand]
    private void DeleteNode()
    {
        WeakReferenceMessenger.Default.Send(new DeleteMessage(""));
    }

    [RelayCommand]
    private void CopyNode()
    {
        WeakReferenceMessenger.Default.Send(new CopyNodeMessage(this));
    }

    [RelayCommand(CanExecute = nameof(NodeBaseHasImagePathField))]
    private void OpenImageDialog()
    {
        WeakReferenceMessenger.Default.Send(new OpenImageDialogMessage(this));
    }

    [RelayCommand(CanExecute = nameof(NodeBaseHasImage))]
    private void RemoveImage()
    {
        Tuple<NodeViewModelBase, string?> nodeAndImagePath = Tuple.Create<NodeViewModelBase, string?>(this, null);
        WeakReferenceMessenger.Default.Send(new SetImageMessage(nodeAndImagePath));
    }

    public abstract NodeViewModelBase Clone();

    public abstract NodeViewModelBase Clone(int zIndex);

    public abstract void Copy(NodeViewModelBase nodeToCopy);

    partial void OnIsSelectedChanged(bool value)
    {
        if (value)
        {
            if (!WeakReferenceMessenger.Default.IsRegistered<MoveNodeMessage>(this))
            {
                WeakReferenceMessenger.Default.Register<MoveNodeMessage>(this, (sender, message) =>
                {
                    NodeBase.PositionX += message.Value.X;
                    NodeBase.PositionY += message.Value.Y;
                });
            }
        }
        else
        {
            if (WeakReferenceMessenger.Default.IsRegistered<MoveNodeMessage>(this))
            {
                WeakReferenceMessenger.Default.Unregister<MoveNodeMessage>(this);
            }
        }
    }
}