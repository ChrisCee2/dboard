using CommunityToolkit.Mvvm.Messaging.Messages;
using mystery_app.Constants;

namespace mystery_app.Messages;
public class ChangePageMessage : ValueChangedMessage<PageConstants.Page>
{
    public ChangePageMessage(PageConstants.Page value) : base(value) { }
}
