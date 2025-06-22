using Avalonia;
using Avalonia.Controls;
using dboard.ViewModels;
using dboard.Views;

namespace dboard.Tools;

static class NodeChangeColorTool
{
    private static bool _isOpen = false;

    public static bool IsOpen()
    {
        return _isOpen;
    }

    public static void ShowColorView(Window parentWindow, PixelPoint positionOnWindow, NodeViewModelBase nodeViewModel)
    {
        Window colorViewWindow = new NodeColorChangeWindowView(new NodeColorChangeViewModel(nodeViewModel));
        // colorViewWindow.Position = positionOnWindow;

        _isOpen = true;
        colorViewWindow.Show(parentWindow);
    }

    public static void ColorViewClosed()
    {
        _isOpen = false;
    }
}