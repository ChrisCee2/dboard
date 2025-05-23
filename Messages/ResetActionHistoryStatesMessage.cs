using CommunityToolkit.Mvvm.Messaging.Messages;
using dboard.Models;

namespace dboard.Messages;
public class ResetActionHistoryStatesMessage : ValueChangedMessage<ResetActionHistoryStatesModel>
{
    public ResetActionHistoryStatesMessage(ResetActionHistoryStatesModel value) : base(value) { }
}
