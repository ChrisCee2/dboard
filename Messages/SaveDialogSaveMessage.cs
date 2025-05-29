using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace dboard.Messages;
public class SaveDialogSaveMessage : ValueChangedMessage<Window>
{
    public SaveDialogSaveMessage(Window value) : base(value) { }
}
