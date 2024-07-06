using Avalonia.Controls;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Input;

namespace mystery_app.Views;

public partial class WorkspaceView : UserControl
{
    public WorkspaceView()
    {
        InitializeComponent();
    }

    public void ToggleNotes(object sender, RoutedEventArgs args)
    {
        NotesSplitView.IsPaneOpen = !NotesSplitView.IsPaneOpen;
    }
}
