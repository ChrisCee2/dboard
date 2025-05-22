using CommunityToolkit.Mvvm.Messaging.Messages;
using dboard.Models;

namespace dboard.Messages;
public class LogActionMessage : ValueChangedMessage<NodeActionModelBase>
{
    public LogActionMessage(NodeActionModelBase value) : base(value) { }
}
