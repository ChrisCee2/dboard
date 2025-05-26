using CommunityToolkit.Mvvm.Messaging.Messages;
using dboard.Models;

namespace dboard.Messages;
public class LogActionMessage : ValueChangedMessage<ActionModelBase>
{
    public LogActionMessage(ActionModelBase value) : base(value) { }
}
