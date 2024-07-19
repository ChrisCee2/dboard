using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Messages;
using mystery_app.ViewModels;

namespace mystery_app.Views;

public partial class WorkspaceView : UserControl
{
    public WorkspaceView()
    {
        InitializeComponent();
        
        WeakReferenceMessenger.Default.Register<SelectNodeEdgeMessage>(this, (sender, message) =>
        {
            ((WorkspaceViewModel)DataContext).EdgeVisualThickness = Constants.Node.EDGE_THICKNESS;
        });
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        ((WorkspaceViewModel)DataContext).EdgeVisualThickness = 0;
        base.OnPointerReleased(e);
    }

    protected override void OnPointerMoved(PointerEventArgs e)
    {
        ((WorkspaceViewModel)DataContext).CursorPosition = e.GetPosition(this);
        base.OnPointerMoved(e);
    }

    public void ToggleNotes(object sender, RoutedEventArgs args)
    {
        NotesSplitView.IsPaneOpen = !NotesSplitView.IsPaneOpen;
    }
}
