using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using dboard.Models;
using dboard.ViewModels;
using dboard.Views;

namespace dboard.Tools;

static class SaveDialogTool
{
    private static bool _isOpen = false;

    public static bool CanShowSaveDialog()
    {
        return !_isOpen;
    }

    // Get point where edge should appear given a node's coordinates and width
    public static Task<bool> ShowSaveDialog(Window parentWindow, SettingsModel sharedSettings)
    {
        Window dialogWindow = new SaveDialogWindowView(new SaveDialogWindowViewModel(sharedSettings));

        var centerX = parentWindow.Position.X + (int)((parentWindow.ClientSize.Width - dialogWindow.Width) / 2);
        var centerY = parentWindow.Position.Y + (int)((parentWindow.ClientSize.Height - dialogWindow.Height) / 2);
        dialogWindow.Position = new PixelPoint(centerX, centerY);

        _isOpen = true;
        return dialogWindow.ShowDialog<bool>(parentWindow);
    }

    public static void SaveDialogClosed()
    {
        _isOpen = false;
    }
}