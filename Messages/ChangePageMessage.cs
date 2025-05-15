using CommunityToolkit.Mvvm.Messaging.Messages;
using dboard.Constants;

namespace dboard.Messages;
public class ChangePageMessage : ValueChangedMessage<PageConstants.PAGE>
{
    public ChangePageMessage(PageConstants.PAGE value) : base(value) { }
}
