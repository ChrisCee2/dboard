using CommunityToolkit.Mvvm.ComponentModel;
using mystery_app.Constants;

namespace mystery_app.Models;

public partial class NodeModel : NodeModelBase
{
    public NodeModel()
    {
        Name = "";
        Desc = "";
        ImagePath = null;
        Width = NodeConstants.MIN_WIDTH;
        Height = NodeConstants.MIN_HEIGHT;
        PositionX = 0;
        PositionY = 0;
        Notes = "";
        ZIndex = 0;
    }

    public NodeModel(int zIndex)
    {
        Name = "";
        Desc = "";
        ImagePath = null;
        Width = NodeConstants.MIN_WIDTH;
        Height = NodeConstants.MIN_HEIGHT;
        PositionX = 0;
        PositionY = 0;
        Notes = "";
        ZIndex = zIndex;
    }

    public NodeModel(
        string name,
        string desc,
        string imagePath,
        double width,
        double height,
        double x,
        double y,
        string notes,
        int zIndex)
    {
        Name = name;
        Desc = desc;
        ImagePath = imagePath;
        Width = width;
        Height = height;
        PositionX = x;
        PositionY = y;
        Notes = notes;
        ZIndex = zIndex;
    }

    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private string? _imagePath;

    [ObservableProperty]
    private string _notes;

    [ObservableProperty]
    private bool _notesToggled;
}
