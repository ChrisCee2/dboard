using CommunityToolkit.Mvvm.Messaging.Messages;
using mystery_app.ViewModels;

namespace mystery_app.Messages;
public class DeleteNodeMessage : ValueChangedMessage<NodeViewModel>
{
    public DeleteNodeMessage(NodeViewModel value) : base(value)
    { 
    }
}
