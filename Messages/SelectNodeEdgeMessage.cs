using CommunityToolkit.Mvvm.Messaging.Messages;
using mystery_app.ViewModels;

namespace mystery_app.Messages;
public class SelectNodeEdgeMessage : ValueChangedMessage<NodeViewModel>
{
    // TODO Can change from string to a custom node class
    public SelectNodeEdgeMessage(NodeViewModel value) : base(value)
    {
    }
}