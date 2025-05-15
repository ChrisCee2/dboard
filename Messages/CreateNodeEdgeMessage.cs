using CommunityToolkit.Mvvm.Messaging.Messages;
using dboard.ViewModels;

namespace dboard.Messages;
public class CreateNodeEdgeMessage : ValueChangedMessage<NodeViewModelBase>
{
    public CreateNodeEdgeMessage(NodeViewModelBase value) : base(value) { }
}