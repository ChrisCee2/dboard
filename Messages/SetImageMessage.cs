using CommunityToolkit.Mvvm.Messaging.Messages;
using dboard.ViewModels;

namespace dboard.Messages;
public class SetImageMessage : ValueChangedMessage<NodeViewModelBase>
{
    public SetImageMessage(NodeViewModelBase value) : base(value) { }
}
