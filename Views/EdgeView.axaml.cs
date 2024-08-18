using Avalonia.Controls;
using Avalonia.Input;
using CommunityToolkit.Mvvm.Messaging;
using mystery_app.Messages;
using mystery_app.ViewModels;

namespace mystery_app.Views;

public partial class EdgeView : Canvas
{
    public EdgeView()
    {
        InitializeComponent();
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        base.OnPointerReleased(e);
        WeakReferenceMessenger.Default.Send(new SelectNodeEdgeMessage((EdgeViewModel)DataContext));
    }
}