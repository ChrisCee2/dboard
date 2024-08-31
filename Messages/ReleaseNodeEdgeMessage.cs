using CommunityToolkit.Mvvm.Messaging.Messages;
using dboard.ViewModels;

namespace dboard.Messages;
public class ReleaseNodeEdgeMessage : ValueChangedMessage<NodeViewModelBase>
{
    public ReleaseNodeEdgeMessage(NodeViewModelBase value) : base(value) { }
}