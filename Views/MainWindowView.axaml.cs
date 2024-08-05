using Avalonia.Controls;
using mystery_app.ViewModels;

namespace mystery_app.Views;

public partial class MainWindowView : Window
{
    public MainWindowView()
    {
        InitializeComponent();
    }

    protected override void OnDataContextEndUpdate()
    {
        base.OnDataContextEndUpdate();
        Closing += ((MainWindowViewModel)DataContext).OnWindowClosing;
    }
}