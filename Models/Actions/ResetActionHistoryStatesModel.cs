using CommunityToolkit.Mvvm.ComponentModel;

namespace dboard.Models;


public partial class ResetActionHistoryStatesModel : ObservableObject
{
    [ObservableProperty]
    private bool _isSave;
    [ObservableProperty]
    private bool _resetActionHistory;

    public ResetActionHistoryStatesModel(bool isSave, bool resetActionHistory)
    {
        IsSave = isSave;
        ResetActionHistory = resetActionHistory;
    }
}