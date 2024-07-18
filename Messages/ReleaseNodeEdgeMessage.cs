using CommunityToolkit.Mvvm.Messaging.Messages;
using mystery_app.ViewModels;

namespace mystery_app.Messages;
public class ReleaseNodeEdgeMessage : ValueChangedMessage<NodeViewModelBase>
{
    public ReleaseNodeEdgeMessage(NodeViewModelBase value) : base(value) { }
}