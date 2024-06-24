using CommunityToolkit.Mvvm.Messaging.Messages;
using mystery_app.ViewModels;

namespace mystery_app.Messages;
public class ReleaseNodeEdgeMessage : ValueChangedMessage<NodeViewModel>
{
    // Can change from string to a custom node class
    public ReleaseNodeEdgeMessage(NodeViewModel value) : base(value)
    {
    }
}