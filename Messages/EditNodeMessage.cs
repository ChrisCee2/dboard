﻿using CommunityToolkit.Mvvm.Messaging.Messages;
using mystery_app.ViewModels;

namespace mystery_app.Messages;
public class EditNodeMessage : ValueChangedMessage<NodeViewModelBase>
{
    public EditNodeMessage(NodeViewModelBase value) : base(value) { }
}
