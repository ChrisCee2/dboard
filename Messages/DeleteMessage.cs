using CommunityToolkit.Mvvm.Messaging.Messages;

namespace dboard.Messages;
public class DeleteMessage : ValueChangedMessage<string>
{
    public DeleteMessage(string value) : base(value) { }
}
