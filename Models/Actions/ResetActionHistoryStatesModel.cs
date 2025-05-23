using CommunityToolkit.Mvvm.ComponentModel;

namespace dboard.Models;


public partial class ResetActionHistoryStatesModel : ObservableObject
{
    [ObservableProperty]
    private bool _isSave;
    [ObservableProperty]
    private bool _resetActionHistory;
    [ObservableProperty]
    private bool _shouldAlwaysBeUnsaved;

    public ResetActionHistoryStatesModel(bool isSave, bool resetActionHistory, bool shouldAlwaysBeUnsaved)
    {
        IsSave = isSave;
        ResetActionHistory = resetActionHistory;
        ShouldAlwaysBeUnsaved = shouldAlwaysBeUnsaved;
    }
}