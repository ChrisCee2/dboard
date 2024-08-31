using Avalonia.Controls;
using Avalonia.Input;
using CommunityToolkit.Mvvm.Messaging;
using dboard.Messages;
using dboard.ViewModels;

namespace dboard.Views;

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