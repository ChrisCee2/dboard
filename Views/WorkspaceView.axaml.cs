using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.LogicalTree;
using Avalonia.VisualTree;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Constants;
using mystery_app.Messages;
using mystery_app.Tools;
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
                ((WorkspaceViewModel)DataContext).UpdateSelection(nodesToSelect: new ObservableCollection<NodeViewModelBase> { args.Value });
                args.Value.IsEdit = true;
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
        if (((WorkspaceViewModel)DataContext).IsMultiSelecting || ((WorkspaceViewModel)DataContext).NodeToCreateEdge != NodeConstants.NULL_NODEVIEWMODEL)
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
            double x0 = Math.Min(context.PressedPosition.X, context.CursorPosition.X);
            double x1 = x0 + Math.Abs(context.PressedPosition.X - context.CursorPosition.X);
            double y0 = Math.Min(context.PressedPosition.Y, context.CursorPosition.Y);
            double y1 = y0 + Math.Abs(context.PressedPosition.Y - context.CursorPosition.Y);
            Point a0 = new Point(x0, y0);
            Point a1 = new Point(x1, y1);

            // Get container for interactive views (nodes)
            var nodeItemsControl = this.Find<ItemsControl>("NodeItemsControl");

            // Find nodes that are within bounds
            var newSelectedNodes = new ObservableCollection<NodeViewModelBase>();
            foreach (ContentPresenter item in nodeItemsControl.GetLogicalChildren())
            {
                InteractiveView node = item.FindDescendantOfType<InteractiveView>();
                NodeViewModelBase nodeContext = (NodeViewModelBase)node.DataContext;
                Point b0 = new Point(nodeContext.NodeBase.PositionX, nodeContext.NodeBase.PositionY);
                Point b1 = new Point(nodeContext.NodeBase.PositionX + node.Bounds.Size.Width, nodeContext.NodeBase.PositionY + node.Bounds.Size.Height);
                if (Geo.RectInRect(a0, a1, b0, b1))
                {
                    newSelectedNodes.Add(nodeContext);
                }
            }

            // Get container for edges
            var edgeItemsControl = this.Find<ItemsControl>("EdgeItemsControl");

            // Find edges that are within bounds
            var newSelectedEdges = new ObservableCollection<EdgeViewModel>();
            foreach (ContentPresenter item in edgeItemsControl.GetLogicalChildren())
            {
                EdgeView edgeView = item.FindDescendantOfType<EdgeView>();
                Line edge = edgeView.FindControl<Line>("Edge");
                EdgeViewModel edgeContext = (EdgeViewModel)edge.DataContext;
                Point line0 = edge.StartPoint;
                Point line1 = edge.EndPoint;
                if (Geo.LineInRect(line0, line1, a0, a1))
                {
                    newSelectedEdges.Add(edgeContext);
                }
            }
            ((WorkspaceViewModel)DataContext).UpdateSelection(nodesToSelect: newSelectedNodes, edgesToSelect: newSelectedEdges);
        }

        context.IsMultiSelecting = false;
        base.OnPointerReleased(e);
    }
}
