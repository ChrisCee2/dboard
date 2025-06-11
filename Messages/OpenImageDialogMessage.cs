using CommunityToolkit.Mvvm.Messaging.Messages;
using dboard.ViewModels;

namespace dboard.Messages;
public class OpenImageDialogMessage : ValueChangedMessage<NodeViewModelBase>
{
    public OpenImageDialogMessage(NodeViewModelBase value) : base(value) { }
}
