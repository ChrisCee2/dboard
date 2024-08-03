using CommunityToolkit.Mvvm.Messaging.Messages;
using mystery_app.Constants;

namespace mystery_app.Messages;
public class ChangePageMessage : ValueChangedMessage<PageConstants.PAGE>
{
    public ChangePageMessage(PageConstants.PAGE value) : base(value) { }
}
