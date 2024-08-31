using CommunityToolkit.Mvvm.Messaging.Messages;
using dboard.ViewModels;

namespace dboard.Messages;
public class SelectNodeMessage : ValueChangedMessage<NodeViewModelBase>
{
    public SelectNodeMessage(NodeViewModelBase value) : base(value) { }
}
