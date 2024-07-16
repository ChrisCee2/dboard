using CommunityToolkit.Mvvm.Messaging.Messages;
using mystery_app.ViewModels;

namespace mystery_app.Messages;
public class SelectNodeEdgeMessage : ValueChangedMessage<NodeViewModelBase>
{
    // TODO Can change from string to a custom node class
    public SelectNodeEdgeMessage(NodeViewModelBase value) : base(value)
    {
    }
}