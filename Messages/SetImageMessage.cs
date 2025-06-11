using System;
using CommunityToolkit.Mvvm.Messaging.Messages;
using dboard.ViewModels;

namespace dboard.Messages;
public class SetImageMessage : ValueChangedMessage<Tuple<NodeViewModelBase, string?>>
{
    public SetImageMessage(Tuple<NodeViewModelBase, string?> value) : base(value) { }
}
