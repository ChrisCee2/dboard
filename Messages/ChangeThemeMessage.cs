using CommunityToolkit.Mvvm.Messaging.Messages;

namespace mystery_app.Messages;
public class ChangeThemeMessage : ValueChangedMessage<string>
{
    public ChangeThemeMessage(string value) : base(value)
    { 
    }
}
