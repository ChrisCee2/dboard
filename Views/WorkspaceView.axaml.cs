using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Logging;
using Avalonia.LogicalTree;
using Avalonia.VisualTree;
using mystery_app.ViewModels;

namespace mystery_app.Views;

public partial class WorkspaceView : UserControl
{

    private Collection<NodeView> _selectedNodes = new Collection<NodeView>();

    public WorkspaceView()
    {
        InitializeComponent();
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        // Multiselect
        // If not left click, return
        if (!e.GetCurrentPoint(this).Properties.IsLeftButtonPressed) { return; }
        var root = (TopLevel)((Visual)e.Source).GetVisualRoot();
        var rootCoordinates = e.GetPosition(root);
        var hitElement = root.InputHitTest(rootCoordinates);
        if (((Control)hitElement).Parent == this)
        {
            ((WorkspaceViewModel)DataContext).IsMultiSelecting = true;
            ((WorkspaceViewModel)DataContext).PressedPosition = e.GetPosition(this);
            ((WorkspaceViewModel)DataContext).MultiSelectVisualThickness = 2;
        }
        else if (((Control)hitElement).Parent is NodeView node && !_selectedNodes.Contains(node))
        {
            foreach (NodeView n in _selectedNodes)
            {
                    n.IsSelected = false;
            }
            _selectedNodes = new Collection<NodeView> { node };
        }
        base.OnPointerPressed(e);
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        // Edge
        ((WorkspaceViewModel)DataContext).EdgeVisualThickness = 0;
        ((WorkspaceViewModel)DataContext).MultiSelectVisualThickness = 0;

        // Multiselect
        if (((WorkspaceViewModel)DataContext).IsMultiSelecting)
        {

            var x1 = Math.Min(((WorkspaceViewModel)DataContext).PressedPosition.X, ((WorkspaceViewModel)DataContext).CursorPosition.X);
            var x2 = x1 + Math.Abs(((WorkspaceViewModel)DataContext).PressedPosition.X - ((WorkspaceViewModel)DataContext).CursorPosition.X);
            var y1 = Math.Min(((WorkspaceViewModel)DataContext).PressedPosition.Y, ((WorkspaceViewModel)DataContext).CursorPosition.Y);
            var y2 = y1 + Math.Abs(((WorkspaceViewModel)DataContext).PressedPosition.Y - ((WorkspaceViewModel)DataContext).CursorPosition.Y);
            var availableNodes = ((WorkspaceViewModel)DataContext).Nodes;
            var newSelectedNodes = new Collection<NodeView>();
            var itemsControl = this.Find<ItemsControl>("NodeItemsControl");
            foreach (ContentPresenter item in itemsControl.GetLogicalChildren())
            {
                NodeView node = (NodeView)item.Child;
                var nodeX = ((NodeViewModelBase)node.DataContext).Position.X;
                var nodeY = ((NodeViewModelBase)node.DataContext).Position.Y;
            if (x1 < nodeX + node.Bounds.Size.Width
                    && x2 > nodeX
                    && y1 < nodeY + node.Bounds.Size.Height
                    && y2 > nodeY)
                {
                    node.IsSelected = true;
                    newSelectedNodes.Add(node);
                    Logger.TryGet(LogEventLevel.Fatal, LogArea.Control)?.Log(this, node.ToString());
                }
            }
            foreach (NodeView node in _selectedNodes)
            {
                if (!newSelectedNodes.Contains(node))
                {
                    node.IsSelected = false;
                }
            }
            _selectedNodes = newSelectedNodes;
        }
        ((WorkspaceViewModel)DataContext).IsMultiSelecting = false;
        base.OnPointerReleased(e);
    }

    protected override void OnPointerMoved(PointerEventArgs e)
    {
        ((WorkspaceViewModel)DataContext).CursorPosition = e.GetPosition(this);
        base.OnPointerMoved(e);
    }

    protected void ToggleNotes(object sender, RoutedEventArgs args)
    {
        NotesSplitView.IsPaneOpen = !NotesSplitView.IsPaneOpen;
    }
}
