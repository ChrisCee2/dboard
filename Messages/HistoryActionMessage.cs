using CommunityToolkit.Mvvm.Messaging.Messages;
using dboard.Constants;

namespace dboard.Messages;
public class HistoryActionMessage : ValueChangedMessage<WorkspaceConstants.HISTORY_ACTION>
{
    public HistoryActionMessage(WorkspaceConstants.HISTORY_ACTION value) : base(value) { }
}
