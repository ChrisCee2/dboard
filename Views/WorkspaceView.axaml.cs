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
                _updateSelectedNodes(new ObservableCollection<NodeViewModelBase> { args.Value });
                args.Value.IsEdit = true;
            }
            else if (!((WorkspaceViewModel)DataContext).SelectedNodes.Contains(args.Value))
            {
                _updateSelectedNodes(new ObservableCollection<NodeViewModelBase> { args.Value });
            }
        });
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        // Multiselect
        // If not left click, return
        var root = (TopLevel)((Visual)e.Source).GetVisualRoot();
        var rootCoordinates = e.GetPosition(root);
        var hitElement = root.InputHitTest(rootCoordinates);
        if (((Control)hitElement).Parent == this)
        {
            if (!e.GetCurrentPoint(this).Properties.IsLeftButtonPressed) { return; }
            ((WorkspaceViewModel)DataContext).IsMultiSelecting = true;
            ((WorkspaceViewModel)DataContext).PressedPosition = e.GetPosition(this);
            ((WorkspaceViewModel)DataContext).MultiSelectThickness = 2;
        }
        base.OnPointerPressed(e);
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        // Edge
        ((WorkspaceViewModel)DataContext).EdgeThickness = 0;
        ((WorkspaceViewModel)DataContext).MultiSelectThickness = 0;

        // Multiselect
        if (((WorkspaceViewModel)DataContext).IsMultiSelecting)
        {
            // Get points of multiselect
            var x1 = Math.Min(((WorkspaceViewModel)DataContext).PressedPosition.X, ((WorkspaceViewModel)DataContext).CursorPosition.X);
            var x2 = x1 + Math.Abs(((WorkspaceViewModel)DataContext).PressedPosition.X - ((WorkspaceViewModel)DataContext).CursorPosition.X);
            var y1 = Math.Min(((WorkspaceViewModel)DataContext).PressedPosition.Y, ((WorkspaceViewModel)DataContext).CursorPosition.Y);
            var y2 = y1 + Math.Abs(((WorkspaceViewModel)DataContext).PressedPosition.Y - ((WorkspaceViewModel)DataContext).CursorPosition.Y);
            // Get all interactive views (Node containers)
            var itemsControl = this.Find<ItemsControl>("NodeItemsControl");

            // Check if each node is within bounds of multiselect and update the selected nodes
            var newSelectedNodes = new ObservableCollection<NodeViewModelBase>();
            foreach (ContentPresenter item in itemsControl.GetLogicalChildren())
            {
                InteractiveView node = (InteractiveView)item.Child;
                var nodeX = ((NodeViewModelBase)node.DataContext).Position.X;
                var nodeY = ((NodeViewModelBase)node.DataContext).Position.Y;
                if (x1 < nodeX + node.Bounds.Size.Width
                    && x2 > nodeX
                    && y1 < nodeY + node.Bounds.Size.Height
                    && y2 > nodeY)
                {
                    newSelectedNodes.Add((NodeViewModelBase)node.DataContext);
                }
            }
            _updateSelectedNodes(newSelectedNodes);
        }

        ((WorkspaceViewModel)DataContext).IsMultiSelecting = false;
        base.OnPointerReleased(e);
    }

    private void _updateSelectedNodes(ObservableCollection<NodeViewModelBase> newSelectedNodes)
    {
        foreach (NodeViewModelBase node in ((WorkspaceViewModel)DataContext).SelectedNodes)
        {
            node.IsSelected = false;
            node.IsEdit = false;
        }

        foreach (NodeViewModelBase node in newSelectedNodes)
        {
            node.IsSelected = true;
            node.IsEdit = false;
        }
        ((WorkspaceViewModel)DataContext).SelectedNodes = newSelectedNodes;
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
