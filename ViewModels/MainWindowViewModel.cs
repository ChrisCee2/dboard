namespace mystery_app.ViewModels;

public class MainWindowViewModel : ViewModelBase
{

    public MainWindowViewModel()
    {
        FirstNode = new NodeViewModel("YO", "DESC");
    }

    public NodeViewModel FirstNode { get; }

}
