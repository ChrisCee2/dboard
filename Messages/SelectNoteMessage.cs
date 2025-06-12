using CommunityToolkit.Mvvm.Messaging.Messages;
using dboard.ViewModels;

namespace dboard.Messages;
public class SelectNoteMessage : ValueChangedMessage<NoteViewModel?>
{
    public SelectNoteMessage(NoteViewModel? value) : base(value) { }
}
