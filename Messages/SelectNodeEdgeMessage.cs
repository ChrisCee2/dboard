using CommunityToolkit.Mvvm.Messaging.Messages;
using mystery_app.ViewModels;

namespace mystery_app.Messages;
public class SelectNodeEdgeMessage : ValueChangedMessage<NodeViewModelBase>
{
    public SelectNodeEdgeMessage(NodeViewModelBase value) : base(value) { }
}