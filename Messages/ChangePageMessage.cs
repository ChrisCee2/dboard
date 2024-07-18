using CommunityToolkit.Mvvm.Messaging.Messages;

namespace mystery_app.Messages;
public class ChangePageMessage : ValueChangedMessage<string>
{
    public ChangePageMessage(string value) : base(value) { }
}
