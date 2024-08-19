using CommunityToolkit.Mvvm.Messaging.Messages;
using mystery_app.ViewModels;

namespace mystery_app.Messages;
public class CreateNodeEdgeMessage : ValueChangedMessage<NodeViewModelBase>
{
    public CreateNodeEdgeMessage(NodeViewModelBase value) : base(value) { }
}