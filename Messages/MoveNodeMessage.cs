using Avalonia;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace mystery_app.Messages;
public class MoveNodeMessage : ValueChangedMessage<Point>
{
    public MoveNodeMessage(Point value) : base(value) { }
}
