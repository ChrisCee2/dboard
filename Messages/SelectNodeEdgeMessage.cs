using CommunityToolkit.Mvvm.Messaging.Messages;
using dboard.ViewModels;

namespace dboard.Messages;
public class SelectNodeEdgeMessage : ValueChangedMessage<EdgeViewModel>
{
    public SelectNodeEdgeMessage(EdgeViewModel value) : base(value) { }
}