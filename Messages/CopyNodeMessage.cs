using CommunityToolkit.Mvvm.Messaging.Messages;
using dboard.ViewModels;

namespace dboard.Messages;
public class CopyNodeMessage : ValueChangedMessage<NodeViewModelBase>
{
    public CopyNodeMessage(NodeViewModelBase value) : base(value) { }
}
