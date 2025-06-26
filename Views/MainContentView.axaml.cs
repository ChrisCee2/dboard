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
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Logging;
using Avalonia.LogicalTree;
using Avalonia.Platform.Storage;
using Avalonia.Styling;
using Avalonia.VisualTree;
using CommunityToolkit.Mvvm.Messaging;
using dboard.Constants;
using dboard.Messages;
using dboard.Models;
using dboard.Tools;
using dboard.ViewModels;

namespace dboard.Views;

public partial class MainContentView : Grid
{

    private bool _isResizingNotes;
    private double _initialResizeX;
    private double _lastNotesLen;
    private SplitView _notesSplitView;
    private Control _workspaceCanvas;

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
        _workspaceCanvas = this.FindControl<WorkspaceView>("CurrentWorkspace").FindControl<Panel>("WorkspaceCanvas");

        if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            ((TopLevel)desktop.MainWindow).AddHandler(InputElement.KeyDownEvent, HandleKeyDown, handledEventsToo: true);
            ((TopLevel)desktop.MainWindow).AddHandler(InputElement.KeyUpEvent, HandleKeyUp, handledEventsToo: true);
        }

        MultiBinding LightAccentMB = new MultiBinding()
        {
            Bindings = [
                new Binding("ActualThemeVariant") { Source=this },
                new Binding() { Source="Light" },
                new Binding() { Path="SharedSettings.UseThemeAccent" },
                ],
            Converter = new Converters.UseThemeConverter()
        };

        MultiBinding DarkAccentMB = new MultiBinding()
        {
            Bindings = [
            new Binding("ActualThemeVariant") { Source=this },
                new Binding() { Source="Dark" },
                new Binding() { Path="SharedSettings.UseThemeAccent" },
                ],
            Converter = new Converters.UseThemeConverter()
        };

        Control settingsBorder = (Control) this.FindControl<SplitView>("SettingsSplitView").Pane;
        Control notesBorder = (Control) this.FindControl<SplitView>("NotesSplitView").Pane;

        // Background bindings
        this.BindClass("LightAccent", LightAccentMB, null);
        this.BindClass("DarkAccent", DarkAccentMB, null);

        settingsBorder.BindClass("LightAccent", LightAccentMB, null);
        settingsBorder.BindClass("DarkAccent", DarkAccentMB, null);

        notesBorder.BindClass("LightAccent", LightAccentMB, null);
        notesBorder.BindClass("DarkAccent", DarkAccentMB, null);

        WeakReferenceMessenger.Default.Register<SaveDialogSaveMessage>(this, (sender, message) =>
        {
            SaveDialogSaveCloseWindow(message.Value);
        });
    }

    protected async void SaveEvent(object sender, RoutedEventArgs e)
    {
        Save();
    }

    protected async void SaveAsEvent(object sender, RoutedEventArgs e)
    {
        SaveAs();
    }

    protected async void Save(Window? windowToCloseAfterSave = null)
    {
        if (((MainContentViewModel)DataContext).WorkspaceFileName == null)
        {
            SaveAs(windowToCloseAfterSave);
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
                _SaveFile(file, windowToCloseAfterSave);
            }
        }
    }

    protected async void SaveAs(Window? windowToCloseAfterSave = null)
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
            _SaveFile(file, windowToCloseAfterSave);
        }
        else if (windowToCloseAfterSave != null)
        {
            windowToCloseAfterSave.Close(false);
        }
    }

    private async void _SaveFile(IStorageFile file, Window? windowToCloseAfterSave)
    {
        MainContentViewModel vm = (MainContentViewModel)DataContext;
        vm.SaveStatus = WorkspaceConstants.SAVE_STATUS.SAVING;
        // Open writing stream from the file.
        await using var stream = await file.OpenWriteAsync();

        WorkspaceViewModel workspaceVM = ((MainContentViewModel)DataContext).Workspace;
        List<NodeModelBase> nodes = workspaceVM.Nodes.Select(x => x.NodeBase).ToList();
        List<EdgeModel> edges = workspaceVM.Edges.Select(x => x.Edge).ToList();
        List<NoteViewModel> noteViewModels = workspaceVM.Notes.Notes.ToList();
        List<NoteModel> notes = new List<NoteModel>();
        foreach (NoteViewModel noteViewModel in noteViewModels)
        {
            noteViewModel.UpdateNoteModel();
            notes.Add(noteViewModel.Note);
        }
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
        await JsonSerializer.SerializeAsync(stream, workspace, options);
        ((MainContentViewModel)DataContext).WorkspaceFileName = file.Name;
        vm.SaveStatus = WorkspaceConstants.SAVE_STATUS.SAVED;
        vm.ResetActionHistoryStates(true, false, false, false);

        if (windowToCloseAfterSave != null)
        {
            windowToCloseAfterSave.Close(true);
        }
    }

    protected void SaveDialogSaveCloseWindow(Window saveDialogWindow)
    {
        Save(saveDialogWindow);
        saveDialogWindow.Hide();
    }

    protected async void ChooseWorkspace(object sender, RoutedEventArgs e)
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
            MainContentViewModel viewModel = (MainContentViewModel)DataContext;
            if (viewModel is not null)
            {
                await using var stream = await files[0].OpenReadAsync();
                viewModel.WorkspaceToLoad = JsonSerializer.Deserialize<WorkspaceModel>(stream, options);
                viewModel.WorkspaceNameToLoad = files[0].Name;
                if (!viewModel.ShouldShowSaveDialog())
                {
                    if (viewModel.WorkspaceToLoad != null && viewModel.WorkspaceNameToLoad != null)
                    {
                        viewModel.LoadWorkspace(viewModel.WorkspaceToLoad, viewModel.WorkspaceNameToLoad);
                    }
                }
                else if (SaveDialogTool.CanShowSaveDialog())
                {
                    if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                    {
                        viewModel.ShouldLoadWorkspace = await SaveDialogTool.ShowSaveDialog(desktop.MainWindow, viewModel.SharedSettings);
                    }

                }
            }
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
            ((MainContentViewModel)DataContext).NotesPaneLength = Math.Min(ToolbarConstants.NOTES_PANE_MAX_LEN, Math.Max(ToolbarConstants.NOTES_PANE_MIN_LEN, _lastNotesLen + offsetX));
        }
    }

    protected void CreateNodeMidScreen(object sender, RoutedEventArgs e)
    {
        if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            Point pos = _workspaceCanvas.PointToClient(new PixelPoint((int)desktop.MainWindow.ClientSize.Width / 2, (int)desktop.MainWindow.ClientSize.Height / 2));
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

        if (e.KeyModifiers.HasFlag(KeyModifiers.Shift))
        {
            ((MainContentViewModel)DataContext).Workspace.MultiSelectHKDown = true;
        }
    }

    private void HandleKeyUp(object sender, KeyEventArgs e)
    {
        if (!(e.KeyModifiers.HasFlag(KeyModifiers.Shift)))
        {
            ((MainContentViewModel)DataContext).Workspace.MultiSelectHKDown = false;
        }
    }

    // Handle zoom
    protected void HandleZoom(object sender, PointerWheelEventArgs e)
    {
        // Make sure these exist and instatiate them as variables
        if (
            DataContext is not MainContentViewModel viewModel || 
            sender is not Control control ||
            Application.Current is not Application app ||
            app.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop ||
            desktop.MainWindow is not Window mainWindow
            )
        {
            return;
        }

        viewModel.Workspace.Scale = Math.Clamp(
            viewModel.Workspace.Scale + (e.Delta.Y * 0.1),
            WorkspaceConstants.MIN_ZOOM,
            WorkspaceConstants.MAX_ZOOM
        );

        if (e.Delta.Y > 0)
        {
            Point cursorPosition = e.GetPosition(control);
            var pan = (mainWindow.Bounds.Center - cursorPosition) * WorkspaceConstants.ZOOM_PAN_EASE * Math.Min(1, 1.0 / viewModel.Workspace.Scale);

            viewModel.Workspace.PanPosition += pan;
        }

        base.OnPointerWheelChanged(e);
    }

    /*  Pan & multiselect logic below */
    private Point _lastPanClickPosition;
    private Point _lastPanPositionSincePanClick;

    // Start multiselect
    protected void WorkspaceOnPointerPressed(object sender, PointerPressedEventArgs e)
    {
        if (DataContext is not MainContentViewModel mcViewModel || mcViewModel.Workspace is not WorkspaceViewModel workspaceViewModel)
        {
            return;
        }

        workspaceViewModel.PressedPosition = e.GetPosition(_workspaceCanvas);
        workspaceViewModel.CursorPosition = e.GetPosition(_workspaceCanvas);

        if (e.GetCurrentPoint(_workspaceCanvas).Properties.IsMiddleButtonPressed && !workspaceViewModel.IsMultiSelecting)
        {
            workspaceViewModel.IsPanning = true;
            _lastPanClickPosition = e.GetPosition((Visual?)Parent);
            _lastPanPositionSincePanClick = workspaceViewModel.PanPosition;
        }
        else if (e.GetCurrentPoint(_workspaceCanvas).Properties.IsLeftButtonPressed && !workspaceViewModel.IsPanning && workspaceViewModel.ClickMode == "Select")
        {
            var root = (TopLevel)((Visual)e.Source).GetVisualRoot();
            var rootCoordinates = e.GetPosition(root);
            var hitElement = root.InputHitTest(rootCoordinates);
            if (((Control)hitElement).Parent == this.Find<WorkspaceView>("CurrentWorkspace"))
            {
                workspaceViewModel.IsMultiSelecting = true;
            }
        }
        base.OnPointerPressed(e);
    }

    protected void WorkspaceOnPointerMoved(object sender, PointerEventArgs e)
    {
        if (DataContext is MainContentViewModel mainContentViewModel && 
            mainContentViewModel.Workspace is WorkspaceViewModel workspaceViewModel)
        {
            workspaceViewModel.CursorPosition = e.GetPosition(_workspaceCanvas);

            if (workspaceViewModel.IsPanning)
            {
                var currentPosition = e.GetPosition((Visual?)Parent);

                Point offset = _lastPanPositionSincePanClick + ((currentPosition - _lastPanClickPosition) / workspaceViewModel.Scale);
                // workspaceViewModel.PanPosition += (new Point(offsetX, offsetY) - workspaceViewModel.PanPosition) / workspaceViewModel.Scale;
                workspaceViewModel.PanPosition = offset;
            }
        }
        base.OnPointerMoved(e);
    }

    protected void WorkspaceOnPointerReleased(object sender, PointerReleasedEventArgs e)
    {
        WorkspaceViewModel context = ((MainContentViewModel)DataContext).Workspace;

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
            Point pos = e.GetPosition(_workspaceCanvas.FindControl<Panel>("WorkspaceCanvas"));
            context.CreateNodeAtPos(pos.X, pos.Y);
        }

        context.IsMultiSelecting = false;
        context.IsPanning = false;
        base.OnPointerReleased(e);
    }
}