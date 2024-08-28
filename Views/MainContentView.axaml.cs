using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Platform.Storage;
using Avalonia.VisualTree;
using mystery_app.Constants;
using mystery_app.Models;
using mystery_app.Tools;
using mystery_app.ViewModels;

namespace mystery_app.Views;

public partial class MainContentView : DockPanel
{

    private bool _isResizingNotes;
    private double _initialResizeX;
    private double _lastNotesLen;
    private SplitView _notesSplitView;
    private WorkspaceView _workspace;

    JsonSerializerOptions options = new()
    {
        ReferenceHandler = ReferenceHandler.Preserve,
        WriteIndented = true
    };

    FilePickerFileType jsonFileType = new FilePickerFileType("json") { Patterns = new[] { "*.json" } };

    public MainContentView()
    {
        InitializeComponent();
        _notesSplitView = this.FindControl<SplitView>("NotesSplitView");
        _workspace = this.FindControl<WorkspaceView>("CurrentWorkspace");

        if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            ((TopLevel)desktop.MainWindow).AddHandler(InputElement.KeyDownEvent, HandleKeyDown, handledEventsToo: true);
            ((TopLevel)desktop.MainWindow).AddHandler(InputElement.KeyUpEvent, HandleKeyUp, handledEventsToo: true);
        }
    }

    protected async void SaveEvent(object sender, RoutedEventArgs e)
    {
        Save();
    }

    protected async void SaveAsEvent(object sender, RoutedEventArgs e)
    {
        SaveAs();
    }

    protected async void Save()
    {
        if (((MainContentViewModel)DataContext).WorkspaceFileName == null)
        {
            SaveAs();
        }
        else
        {
            if (!Directory.Exists("./Workspaces"))
            {
                Directory.CreateDirectory("./Workspaces");
            }
            IStorageFolder directory = await TopLevel.GetTopLevel(this).StorageProvider.TryGetFolderFromPathAsync("./Workspaces");
            string path = directory.TryGetLocalPath() + "/" + ((MainContentViewModel)DataContext).WorkspaceFileName;
            IStorageFile file = await TopLevel.GetTopLevel(this).StorageProvider.TryGetFileFromPathAsync(path);
            if (file is not null)
            {
                _SaveFile(file);
            }
        }
    }

    protected async void SaveAs()
    {
        if (!Directory.Exists("./Workspaces"))
        {
            Directory.CreateDirectory("./Workspaces");
        }

        IStorageFolder directory = await TopLevel.GetTopLevel(this).StorageProvider.TryGetFolderFromPathAsync("./Workspaces");

        IStorageFile file = await TopLevel.GetTopLevel(this).StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "Save Workspace",
            SuggestedFileName = "Workspace",
            SuggestedStartLocation = directory,
            DefaultExtension = "json"
        });

        if (file is not null)
        {
            _SaveFile(file);
        }
    }

    private async void _SaveFile(IStorageFile file)
    {
        // Open writing stream from the file.
        await using var stream = await file.OpenWriteAsync();

        WorkspaceViewModel workspaceVM = ((MainContentViewModel)DataContext).Workspace;
        List<NodeModelBase> nodes = workspaceVM.Nodes.Select(x => x.NodeBase).ToList();
        List<EdgeModel> edges = workspaceVM.Edges.Select(x => x.Edge).ToList();
        NotesModel notes = ((MainContentViewModel)DataContext).Notes;
        WorkspaceModel workspace = new WorkspaceModel(
            nodes, 
            edges, 
            notes, 
            workspaceVM.CanvasSizeX, 
            workspaceVM.CanvasSizeY,
            workspaceVM.WorkspaceSizeX,
            workspaceVM.WorkspaceSizeY,
            workspaceVM.CanvasImagePath, 
            workspaceVM.WorkspaceImagePath,
            workspaceVM.WindowImagePath);
        JsonSerializer.SerializeAsync(stream, workspace, options);
        ((MainContentViewModel)DataContext).WorkspaceFileName = file.Name;
    }

    protected async void Open(object sender, RoutedEventArgs e)
    {
        if (!Directory.Exists("./Workspaces"))
        {
            Directory.CreateDirectory("./Workspaces");
        }

        IStorageFolder directory = await TopLevel.GetTopLevel(this).StorageProvider.TryGetFolderFromPathAsync("./Workspaces");

        var files = await TopLevel.GetTopLevel(this).StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Workspace",
            AllowMultiple = false,
            SuggestedStartLocation = directory,
            FileTypeFilter = new[] { jsonFileType }
        });

        if (files.Count == 1)
        {
            ((MainContentViewModel)DataContext).NewCommand.Execute(null);
            await using var stream = await files[0].OpenReadAsync();
            WorkspaceModel workspace = JsonSerializer.Deserialize<WorkspaceModel>(stream, options);
            foreach (var node in workspace.Nodes)
            {
                if (node is NodeModel nodeModel)
                {
                    ((MainContentViewModel)DataContext).Workspace.Nodes.Add(new NodeViewModel(nodeModel));
                }
            }
            foreach (EdgeModel edgeModel in workspace.Edges)
            {
                ((MainContentViewModel)DataContext).Workspace.Edges.Add(new EdgeViewModel(edgeModel));
            }
            ((MainContentViewModel)DataContext).Notes = workspace.Notes;
            ((MainContentViewModel)DataContext).WorkspaceFileName = files[0].Name;
            ((MainContentViewModel)DataContext).Workspace.CanvasSizeX = workspace.CanvasSizeX;
            ((MainContentViewModel)DataContext).Workspace.CanvasSizeY = workspace.CanvasSizeY;
            ((MainContentViewModel)DataContext).Workspace.WorkspaceSizeX = workspace.WorkspaceSizeX;
            ((MainContentViewModel)DataContext).Workspace.WorkspaceSizeY = workspace.WorkspaceSizeY;
            ((MainContentViewModel)DataContext).Workspace.CanvasImagePath = workspace.CanvasImagePath;
            ((MainContentViewModel)DataContext).Workspace.WorkspaceImagePath = workspace.WorkspaceImagePath;
            ((MainContentViewModel)DataContext).Workspace.WindowImagePath = workspace.WindowImagePath;
        }
    }

    protected void Exit(object sender, RoutedEventArgs e)
    {
        if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow.Close();
        }
    }

    protected void Minimize(object sender, RoutedEventArgs e)
    {
        if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow.WindowState = WindowState.Minimized;
        }
    }

    protected void Maximize(object sender, RoutedEventArgs e)
    {
        ToggleMaximize();
    }

    protected void MoveWindow(object sender, PointerPressedEventArgs e)
    {
        if (!e.GetCurrentPoint(Parent as Visual).Properties.IsLeftButtonPressed) { return; }
        if (e.ClickCount >= 2)
        {
            ToggleMaximize();
            return;
        }
        if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow.BeginMoveDrag(e);
        }
    }

    protected void ToggleMaximize()
    {
        if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            if (desktop.MainWindow.WindowState == WindowState.Maximized)
            {
                desktop.MainWindow.WindowState = WindowState.Normal;
            }
            else
            {
                desktop.MainWindow.WindowState = WindowState.Maximized;
            }
        }
    }

    // On selecting resize button, get current cursor position and set axis to resize
    private void ResizePointerPressed(object sender, PointerPressedEventArgs e)
    {
        // If not left click, return
        if (!e.GetCurrentPoint(Parent as Visual).Properties.IsLeftButtonPressed) { return; }
        _isResizingNotes = true;
        var pos = e.GetPosition((Visual?)Parent);
        _initialResizeX = pos.X;
        _lastNotesLen = _notesSplitView.OpenPaneLength;
    }

    protected void ResizePointerReleased(object sender, PointerReleasedEventArgs e)
    {
        _isResizingNotes = false;
    }

    protected void ResizePointerMoved(object sender, PointerEventArgs e)
    {
        if (Parent == null || !_isResizingNotes) { return; }
        Point currentPosition = e.GetPosition((Visual?)Parent);

        if (_isResizingNotes)
        {
            // Offset found by subtracting original cursor position on resize press from the current cursor position
            double offsetX = _initialResizeX - currentPosition.X;
            ((MainContentViewModel)DataContext).Notes.PaneLength = Math.Min(ToolbarConstants.NOTES_PANE_MAX_LEN, Math.Max(ToolbarConstants.NOTES_PANE_MIN_LEN, _lastNotesLen + offsetX));
        }
    }

    protected void CreateNodeMidScreen(object sender, RoutedEventArgs e)
    {
        if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            Point pos = _workspace.PointToClient(new PixelPoint((int)desktop.MainWindow.ClientSize.Width / 2, (int)desktop.MainWindow.ClientSize.Height / 2));
            ((MainContentViewModel)DataContext).Workspace.CreateNodeAtPos(pos.X, pos.Y);
        }
    }

    private void HandleKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Source is WorkspaceView && e.KeyModifiers.HasFlag(KeyModifiers.Control))
        {
            if (e.Key == Key.C)
            {
                ((MainContentViewModel)DataContext).Workspace.CopyNodesCommand.Execute(null);
            }
            else if (e.Key == Key.V)
            {
                ((MainContentViewModel)DataContext).Workspace.PasteNodesCommand.Execute(null);
            }
            else if (e.Key == Key.X)
            {
                ((MainContentViewModel)DataContext).Workspace.DeleteSelectedItemsCommand.Execute(null);
            }
            else if (e.Key == Key.S)
            {
                Save();
            }
        }

        if (e.KeyModifiers.HasFlag(KeyModifiers.Control) || e.KeyModifiers.HasFlag(KeyModifiers.Shift))
        {
            ((MainContentViewModel)DataContext).Workspace.MultiSelectHKDown = true;
        }
    }

    private void HandleKeyUp(object sender, KeyEventArgs e)
    {
        if (!(e.KeyModifiers.HasFlag(KeyModifiers.Control) || e.KeyModifiers.HasFlag(KeyModifiers.Shift)))
        {
            ((MainContentViewModel)DataContext).Workspace.MultiSelectHKDown = false;
        }
    }

    // Handle zoom
    protected void HandleZoom(object sender, PointerWheelEventArgs e)
    {
        ((MainContentViewModel)DataContext).Workspace.Scale = Math.Clamp(((MainContentViewModel)DataContext).Workspace.Scale + (e.Delta.Y * 0.1), 0.3, 5);
        base.OnPointerWheelChanged(e);
    }

    /*  Pan & multiselect logic below */
    private Point _positionInBlock;

    // Start multiselect
    protected void WorkspaceOnPointerPressed(object sender, PointerPressedEventArgs e)
    {
        WorkspaceViewModel context = ((MainContentViewModel)DataContext).Workspace;

        context.PressedPosition = e.GetPosition(_workspace);
        context.CursorPosition = e.GetPosition(_workspace);

        if (e.GetCurrentPoint(_workspace).Properties.IsMiddleButtonPressed && !context.IsMultiSelecting)
        {
            context.IsPanning = true;
            var pos = e.GetPosition((Visual?)Parent);
            _positionInBlock = new Point(pos.X - ((int)context.PanPosition.X), pos.Y - ((int)context.PanPosition.Y));
        }
        else if (e.GetCurrentPoint(_workspace).Properties.IsLeftButtonPressed && !context.IsPanning && context.ClickMode == "Select")
        {
            var root = (TopLevel)((Visual)e.Source).GetVisualRoot();
            var rootCoordinates = e.GetPosition(root);
            var hitElement = root.InputHitTest(rootCoordinates);
            if (((Control)hitElement).Parent == this.Find<WorkspaceView>("CurrentWorkspace"))
            {
                context.IsMultiSelecting = true;
                context.MultiSelectThickness = 2;
            }
        }
        base.OnPointerPressed(e);
    }

    protected void WorkspaceOnPointerMoved(object sender, PointerEventArgs e)
    {
        WorkspaceViewModel context = ((MainContentViewModel)DataContext).Workspace;

        // Only update position if multiselecting or edge selecting
        if (context.IsMultiSelecting || context.NodeToCreateEdge != NodeConstants.NULL_NODEVIEWMODEL)
        {
            context.CursorPosition = e.GetPosition(_workspace);
        }
        else if (context.IsPanning)
        {
            var currentPosition = e.GetPosition((Visual?)Parent);

            var offsetX = currentPosition.X - _positionInBlock.X;
            var offsetY = currentPosition.Y - _positionInBlock.Y;
            context.PanPosition = new Point(offsetX, offsetY);
        }
        base.OnPointerMoved(e);
    }

    protected void WorkspaceOnPointerReleased(object sender, PointerReleasedEventArgs e)
    {
        WorkspaceViewModel context = ((MainContentViewModel)DataContext).Workspace;
        // Make lines disappear
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
            var nodeItemsControl = this.Find<WorkspaceView>("CurrentWorkspace").Find<ItemsControl>("NodeItemsControl");

            // Find nodes that are within bounds
            var newSelectedNodes = new ObservableCollection<NodeViewModelBase>();
            foreach (ContentPresenter item in nodeItemsControl.GetLogicalChildren())
            {
                InteractiveView node = item.FindDescendantOfType<InteractiveView>();
                NodeViewModelBase nodeContext = (NodeViewModelBase)node.DataContext;
                Point b0 = new Point(nodeContext.NodeBase.PositionX, nodeContext.NodeBase.PositionY + (EdgeConstants.distFromEdgeToNodePos * 2)); // Offset to account for edge occupation of interactive view
                Point b1 = new Point(nodeContext.NodeBase.PositionX + node.Bounds.Size.Width, nodeContext.NodeBase.PositionY + node.Bounds.Size.Height);
                if (Geo.RectInRect(a0, a1, b0, b1))
                {
                    newSelectedNodes.Add(nodeContext);
                }
            }

            // Get container for edges
            var edgeItemsControl = this.Find<WorkspaceView>("CurrentWorkspace").Find<ItemsControl>("EdgeItemsControl");

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
            context.UpdateSelection(nodesToSelect: newSelectedNodes, edgesToSelect: newSelectedEdges);
        }
        else if (context.ClickMode == "CreateNode" && e.InitialPressMouseButton.Equals(MouseButton.Left))
        {
            Point pos = e.GetPosition(_workspace);
            context.CreateNodeAtPos(pos.X, pos.Y);
        }

        context.IsMultiSelecting = false;
        context.IsPanning = false;
        base.OnPointerReleased(e);
    }
}