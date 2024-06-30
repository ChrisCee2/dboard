using CommunityToolkit.Mvvm.Messaging.Messages;
using mystery_app.Models;

namespace mystery_app.Messages;

public class MoveNodeMessage : ValueChangedMessage<MoveNodeHelper>
{
    public MoveNodeMessage(MoveNodeHelper value) : base(value) { }
}