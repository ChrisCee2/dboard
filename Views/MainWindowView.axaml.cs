using Avalonia;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using dboard.Messages;
using dboard.Tools;
using dboard.ViewModels;

namespace dboard.Views;

public partial class MainWindowView : Window
{
    public MainWindowView()
    {
        InitializeComponent();
        WeakReferenceMessenger.Default.Register<CloseAppMessage>(this, (sender, message) =>
        {
            Close();
        });

        WeakReferenceMessenger.Default.Register<ChangeColorMessage>(this, (sender, message) =>
        {
            NodeViewModelBase viewModel = message.Value;
            if (!NodeChangeColorTool.IsOpen())
            {
                PixelPoint point = new PixelPoint((int) viewModel.NodeBase.PositionX, (int) viewModel.NodeBase.PositionY);
                NodeChangeColorTool.ShowColorView(this, point, viewModel);
            }
            // Create an instance of color view window with binding to the noteviewmodel
            //NoteViewModel val = message.Value;
            //ResetActionHistoryStates(val.IsSave, val.ResetActionHistory, val.ShouldAlwaysBeUnsaved, val.IsNew);
        });
    }

    protected override void OnDataContextEndUpdate()
    {
        base.OnDataContextEndUpdate();
        Closing += OnWindowClosing;
    }

    public async void OnWindowClosing(object sender, WindowClosingEventArgs e)
    {
        MainWindowViewModel viewModel = (MainWindowViewModel)DataContext;
        if (viewModel != null)
        {
            viewModel.SaveSettings();
            if (viewModel.ShouldClose == true || !viewModel.ShouldShowSaveDialog())
            {
                return;
            }
            else if (SaveDialogTool.CanShowSaveDialog())
            {
                e.Cancel = true;

                viewModel.ShouldClose = await SaveDialogTool.ShowSaveDialog(this, viewModel.SharedSettings);
            }
            e.Cancel = true;
        }
    }
}