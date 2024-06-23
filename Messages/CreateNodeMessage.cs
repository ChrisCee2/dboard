using CommunityToolkit.Mvvm.Messaging.Messages;

namespace mystery_app.Messages;
public class CreateNodeMessage : ValueChangedMessage<string>
{
    // Can change from string to a custom node class
    public CreateNodeMessage(string value) : base(value)
    { 
    }
}
