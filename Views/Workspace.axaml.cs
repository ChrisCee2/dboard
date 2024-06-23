using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using mystery_app.ViewModels;

namespace mystery_app.Views;

public partial class Workspace : UserControl
{
    public Workspace()
    {
        InitializeComponent();
    }

    private NodeViewModel _selectedNode;

    public void helloworld() { return; }

    private void PointerPressedHandler(object sender, PointerPressedEventArgs args)
    {
        //// If not left click, return
        //if (!e.GetCurrentPoint(Parent as Avalonia.Visual).Properties.IsLeftButtonPressed) { return; }
        //_isPressed = true;
        //base.OnPointerPressed(e);

        var point = args.GetCurrentPoint(sender as Control);
        var x = point.Position.X;
        var y = point.Position.Y;
        var msg = $"Pointer press at {x}, {y} relative to sender.";
        if (point.Properties.IsLeftButtonPressed)
        {
            msg += " Left button pressed.";
        }
        if (point.Properties.IsRightButtonPressed)
        {
            msg += " Right button pressed.";
        }

        if (sender is Node node)
        {
            results.Text = "YO";
            return;
        }
        if (args == null) { return; }
        results.Text = args.Source.ToString();
        //var a = sender;
        //object data = a.DataContext;
        //results.Text = data.ToString();
    }

}