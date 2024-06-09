using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace mystery_app.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{

    public MainWindowViewModel()
    {
        Nodes = new ObservableCollection<NodeViewModel>(new List<NodeViewModel>());
    }

    [RelayCommand]
    private void CreateNode()
    {
        Nodes.Add(new NodeViewModel("1", "2"));
    }

    public ObservableCollection<NodeViewModel> Nodes { get; set; }

}
