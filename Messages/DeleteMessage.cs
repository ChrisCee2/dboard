using CommunityToolkit.Mvvm.Messaging.Messages;

namespace mystery_app.Messages;
public class DeleteMessage : ValueChangedMessage<string>
{
    public DeleteMessage(string value) : base(value) { }
}
