using CommunityToolkit.Mvvm.Messaging.Messages;
using dboard.ViewModels;

namespace dboard.Messages;
public class ChangeColorMessage : ValueChangedMessage<NodeViewModelBase>
{
    public ChangeColorMessage(NodeViewModelBase value) : base(value) { }
}
