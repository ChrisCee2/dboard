using CommunityToolkit.Mvvm.ComponentModel;
using dboard.Models;

namespace dboard.ViewModels;

public partial class SaveDialogWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private SettingsModel _sharedSettings;

    public SaveDialogWindowViewModel(SettingsModel sharedSettings)
    {
        SharedSettings = sharedSettings;
    }
}
