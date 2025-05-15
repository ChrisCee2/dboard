using CommunityToolkit.Mvvm.Messaging.Messages;
using dboard.ViewModels;

namespace dboard.Messages;
public class EditNodeMessage : ValueChangedMessage<NodeViewModelBase>
{
    public EditNodeMessage(NodeViewModelBase value) : base(value) { }
}
