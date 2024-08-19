using CommunityToolkit.Mvvm.Messaging.Messages;
using mystery_app.ViewModels;

namespace mystery_app.Messages;
public class SelectNodeEdgeMessage : ValueChangedMessage<EdgeViewModel>
{
    public SelectNodeEdgeMessage(EdgeViewModel value) : base(value) { }
}