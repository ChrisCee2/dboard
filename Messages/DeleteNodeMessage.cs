using CommunityToolkit.Mvvm.Messaging.Messages;
using mystery_app.ViewModels;

namespace mystery_app.Messages;
public class DeleteNodeMessage : ValueChangedMessage<NodeViewModelBase>
{
    public DeleteNodeMessage(NodeViewModelBase value) : base(value) { }
}
