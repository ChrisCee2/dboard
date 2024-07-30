using CommunityToolkit.Mvvm.Messaging.Messages;
using mystery_app.ViewModels;

namespace mystery_app.Messages;
public class CopyNodeMessage : ValueChangedMessage<NodeViewModelBase>
{
    public CopyNodeMessage(NodeViewModelBase value) : base(value) { }
}
