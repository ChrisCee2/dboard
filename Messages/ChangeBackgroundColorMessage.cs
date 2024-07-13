using CommunityToolkit.Mvvm.Messaging.Messages;

namespace mystery_app.Messages;
public class ChangeBackgroundColorMessage : ValueChangedMessage<string>
{
    public ChangeBackgroundColorMessage(string value) : base(value)
    { 
    }
}
