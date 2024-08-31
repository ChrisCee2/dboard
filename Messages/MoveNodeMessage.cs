using Avalonia;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace dboard.Messages;
public class MoveNodeMessage : ValueChangedMessage<Point>
{
    public MoveNodeMessage(Point value) : base(value) { }
}
