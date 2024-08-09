using CommunityToolkit.Mvvm.ComponentModel;
using mystery_app.Constants;

namespace mystery_app.Models;

public partial class NodeModel : NodeModelBase
{
    public NodeModel()
    {
        Name = "";
        Desc = "";
        ImagePath = NodeConstants.DEFAULT_IMAGE_PATH;
        Width = NodeConstants.MIN_WIDTH;
        Height = NodeConstants.MIN_HEIGHT;
        PositionX = 0;
        PositionY = 0;
        Notes = "";
    }

    public NodeModel(
        string name,
        string desc,
        string imagePath,
        double width,
        double height,
        double x,
        double y,
        string notes)
    {
        Name = name;
        Desc = desc;
        ImagePath = imagePath;
        Width = width;
        Height = height;
        PositionX = x;
        PositionY = y;
        Notes = notes;
    }

    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private string _imagePath;

    [ObservableProperty]
    private string _notes;

    [ObservableProperty]
    private bool _notesToggled;
}
