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
    [ObservableProperty]
    private bool _isNew;

    public ResetActionHistoryStatesModel(bool isSave, bool resetActionHistory, bool shouldAlwaysBeUnsaved, bool isNew)
    {
        IsSave = isSave;
        ResetActionHistory = resetActionHistory;
        ShouldAlwaysBeUnsaved = shouldAlwaysBeUnsaved;
        IsNew = isNew;
    }
}