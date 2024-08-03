using CommunityToolkit.Mvvm.Messaging.Messages;
using mystery_app.ViewModels;

namespace mystery_app.Messages;
public class SelectNodeMessage : ValueChangedMessage<NodeViewModelBase>
{
    public SelectNodeMessage(NodeViewModelBase value) : base(value) { }
}
