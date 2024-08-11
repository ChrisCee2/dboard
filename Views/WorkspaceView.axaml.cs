using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.VisualTree;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Constants;
using mystery_app.Messages;
using mystery_app.ViewModels;

namespace mystery_app.Views;

public partial class WorkspaceView : UserControl
{
    public WorkspaceView()
    {
        InitializeComponent();

        WeakReferenceMessenger.Default.Register<SelectNodeMessage>(this, (sender, args) =>
        {
            if (args.Value.IsEdit)
            {
                ((WorkspaceViewModel)DataContext).UpdateSelectedNodes(new ObservableCollection<NodeViewModelBase> { args.Value });
                args.Value.IsEdit = true;
            }
            else if (!((WorkspaceViewModel)DataContext).SelectedNodes.Contains(args.Value))
            {
                ((WorkspaceViewModel)DataContext).UpdateSelectedNodes(new ObservableCollection<NodeViewModelBase> { args.Value });
            }
        });
    }

    // Start multiselect
    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        // If not left click, return
        if (!e.GetCurrentPoint(this).Properties.IsLeftButtonPressed) { return; }

        var root = (TopLevel)((Visual)e.Source).GetVisualRoot();
        var rootCoordinates = e.GetPosition(root);
        var hitElement = root.InputHitTest(rootCoordinates);
        if (((Control)hitElement).Parent == this)
        {
            ((WorkspaceViewModel)DataContext).IsMultiSelecting = true;
            ((WorkspaceViewModel)DataContext).PressedPosition = e.GetPosition(this);
            ((WorkspaceViewModel)DataContext).CursorPosition = e.GetPosition(this);
            ((WorkspaceViewModel)DataContext).MultiSelectThickness = 2;
        }
        base.OnPointerPressed(e);
    }

    protected override void OnPointerMoved(PointerEventArgs e)
    {
        // Only update position if multiselecting or edge selecting
        if (((WorkspaceViewModel)DataContext).IsMultiSelecting || ((WorkspaceViewModel)DataContext).SelectedNodeEdge != NodeConstants.NULL_NODEVIEWMODEL)
        {
            ((WorkspaceViewModel)DataContext).CursorPosition = e.GetPosition(this);
        }
        base.OnPointerMoved(e);
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        WorkspaceViewModel context = (WorkspaceViewModel)DataContext;
        // Make lines disappear
        context.EdgeThickness = 0;
        context.MultiSelectThickness = 0;

        // Multiselect
        if (context.IsMultiSelecting)
        {
            // Get points of multiselect
            var x1 = Math.Min(context.PressedPosition.X, context.CursorPosition.X);
            var x2 = x1 + Math.Abs(context.PressedPosition.X - context.CursorPosition.X);
            var y1 = Math.Min(context.PressedPosition.Y, context.CursorPosition.Y);
            var y2 = y1 + Math.Abs(context.PressedPosition.Y - context.CursorPosition.Y);
            // Get all interactive views (Node containers)
            var itemsControl = this.Find<ItemsControl>("NodeItemsControl");

            // Check if each node is within bounds of multiselect and update the selected nodes
            var newSelectedNodes = new ObservableCollection<NodeViewModelBase>();
            foreach (ContentPresenter item in itemsControl.GetLogicalChildren())
            {
                InteractiveView node = item.FindDescendantOfType<InteractiveView>();
                NodeViewModelBase nodeContext = (NodeViewModelBase)node.DataContext;
                if (x1 < nodeContext.NodeBase.PositionX + node.Bounds.Size.Width
                    && x2 > nodeContext.NodeBase.PositionX
                    && y1 < nodeContext.NodeBase.PositionY + node.Bounds.Size.Height
                    && y2 > nodeContext.NodeBase.PositionY)
                {
                    newSelectedNodes.Add(nodeContext);
                }
            }
            ((WorkspaceViewModel)DataContext).UpdateSelectedNodes(newSelectedNodes);
        }

        context.IsMultiSelecting = false;
        base.OnPointerReleased(e);
    }
}
