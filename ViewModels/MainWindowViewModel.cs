namespace mystery_app.ViewModels;

public class MainWindowViewModel : ViewModelBase
{

    public MainWindowViewModel()
    {
        FirstNode = new NodeViewModel("YO", "DESC", 100, 30);
    }

    public NodeViewModel FirstNode { get; }

}
