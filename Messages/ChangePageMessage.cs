using CommunityToolkit.Mvvm.Messaging.Messages;

namespace mystery_app.Messages;
public class ChangePageMessage : ValueChangedMessage<string>
{
    // Can change from string to a custom node class
    public ChangePageMessage(string value) : base(value)
    { 
    }
}
