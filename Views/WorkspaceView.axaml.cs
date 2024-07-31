using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.VisualTree;
using mystery_app.ViewModels;

namespace mystery_app.Views;

public partial class WorkspaceView : UserControl
{
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
        else if (((Control)hitElement).Parent is NodeView node && !((WorkspaceViewModel)DataContext).SelectedNodes.Contains((NodeViewModelBase)node.DataContext))
        {
            foreach (NodeViewModelBase n in ((WorkspaceViewModel)DataContext).SelectedNodes)
            {
                n.IsSelected = false;
            }
            ((WorkspaceViewModel)DataContext).SelectedNodes = new ObservableCollection<NodeViewModelBase> { (NodeViewModelBase)node.DataContext };
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
            var newSelectedNodes = new ObservableCollection<NodeViewModelBase>();
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
                    newSelectedNodes.Add((NodeViewModelBase)node.DataContext);
                    ((NodeViewModelBase)node.DataContext).IsSelected = true;
                }
            }
            foreach (NodeViewModelBase node in ((WorkspaceViewModel)DataContext).SelectedNodes)
            {
                if (!newSelectedNodes.Contains(node))
                {
                    node.IsSelected = false;
                }
            }
            ((WorkspaceViewModel)DataContext).SelectedNodes = newSelectedNodes;
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
