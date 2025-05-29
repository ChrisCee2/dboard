using CommunityToolkit.Mvvm.Messaging.Messages;

namespace dboard.Messages;
public class CloseAppMessage : ValueChangedMessage<string>
{
    public CloseAppMessage(string value) : base(value) { }
}
