using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.VisualTree;
using dboard.ViewModels;

namespace dboard.Views;

public partial class NotesView : UserControl
{
    public NotesView()
    {
        InitializeComponent();
    }

    private void PointerPressedHandler(object sender, PointerPressedEventArgs args)
    {
        if (
            sender is not Control control ||
            !args.GetCurrentPoint(control).Properties.IsLeftButtonPressed ||
            DataContext is not NotesViewModel notesViewModel)
        {
            return;
        }
        if (control.DataContext is NoteViewModel noteViewModel)
        {
            notesViewModel.NoteToMove = noteViewModel;
        }
    }

    private void PointerMovedHandler(object sender, PointerEventArgs args)
    {
        if (sender is not Visual visual ||
            visual.GetVisualRoot() is not TopLevel root ||
            !args.GetCurrentPoint(visual).Properties.IsLeftButtonPressed)
        {
            return;
        }

        Point rootCoordinates = args.GetPosition(root);
        if (root.InputHitTest(rootCoordinates) is not Control hitElement)
        {
            return;
        }

        Control? noteContainer = hitElement;
        if (noteContainer.Tag is null || noteContainer.Tag.ToString() != "NoteContainer")
        {
            noteContainer = GetParentWithTag(noteContainer, "NoteContainer");
        }

        if (noteContainer is null || 
            noteContainer.DataContext is not NoteViewModel noteViewModel||
            DataContext is not NotesViewModel notesViewModel) 
        { 
            return; 
        }

        notesViewModel.LastNoteCursorWasOver = noteViewModel;

        double cursorY = args.GetCurrentPoint(noteContainer).Position.Y;
        // Check if cursor is on top half of note
        notesViewModel.CursorIsAboveCurrentNote = cursorY < noteContainer.Bounds.Size.Height / 2.0;
    }

    private void PointerReleasedHandler(object sender, PointerReleasedEventArgs args)
    {
        if (
            args.InitialPressMouseButton != MouseButton.Left ||
            DataContext is not NotesViewModel notesViewModel)
        {
            return;
        }
        notesViewModel.MoveNote();
    }

    private Control? GetParentWithTag(Control control, string tag)
    {
        if (control.GetVisualParent() is not Visual visual)
        {
            return null;
        }
        Control? currentControl = (Control) visual;

        while (currentControl != null)
        {
            if (currentControl.Tag is not null && currentControl.Tag.ToString() == tag)
            {
                return currentControl;
            }

            if (currentControl.GetVisualParent() is not Visual visualParent)
            {
                return null;
            }
            currentControl = (Control) visualParent;
        }

        return null;
    }
}