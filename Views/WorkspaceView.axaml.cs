using System.Collections.ObjectModel;
using System.Reflection;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Messaging;
using dboard.Messages;
using dboard.Models.Actions.Node;
using dboard.ViewModels;

namespace dboard.Views;

public partial class WorkspaceView : UserControl
{
    public WorkspaceView()
    {
        InitializeComponent();

        WeakReferenceMessenger.Default.Register<SelectNodeMessage>(this, (sender, args) =>
        {
            if (args.Value.IsEdit)
            {
                return;
            }
            else if (!((WorkspaceViewModel)DataContext).SelectedNodes.Contains(args.Value))
            {
                ((WorkspaceViewModel)DataContext).UpdateSelection(nodesToSelect: new ObservableCollection<NodeViewModelBase> { args.Value });
            }
        });

        WeakReferenceMessenger.Default.Register<SelectNodeEdgeMessage>(this, (sender, args) =>
        {
            ((WorkspaceViewModel)DataContext).UpdateSelection(edgesToSelect: new ObservableCollection<EdgeViewModel> { args.Value });
        });

        WeakReferenceMessenger.Default.Register<SetImageMessage>(this, (sender, message) =>
        {
            SetImage(message.Value);
        });
    }

    public async void SetImage(NodeViewModelBase nodeViewModel)
    {
        var files = await TopLevel.GetTopLevel(this).StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Insert Image",
            AllowMultiple = false
        });

        if (files.Count == 1)
        {
            if (nodeViewModel.NodeBase.GetType().GetProperty("ImagePath") is PropertyInfo property)
            {
                WeakReferenceMessenger.Default.Send(new LogActionMessage(new EditNodeActionModel(nodeViewModel)));
                property.SetValue(nodeViewModel.NodeBase, files[0].Path.LocalPath, null);
            }
        }
    }
}
