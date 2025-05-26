using CommunityToolkit.Mvvm.ComponentModel;
using dboard.Constants;

namespace dboard.Models;

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

    public override NodeModel Clone()
    {
        return new NodeModel(
            Name,
            Desc,
            ImagePath,
            Width,
            Height,
            PositionX,
            PositionY,
            Notes,
            ZIndex
        );
    }

    public override void Copy(NodeModelBase nodeModelToCopy)
    {
        NodeModel nodeModel = (NodeModel)nodeModelToCopy;
        Name = nodeModel.Name;
        Desc = nodeModel.Desc;
        ImagePath = nodeModel.ImagePath;
        Width = nodeModel.Width;
        Height = nodeModel.Height;
        PositionX = nodeModel.PositionX;
        PositionY = nodeModel.PositionY;
        Notes = nodeModel.Notes;
        ZIndex = nodeModel.ZIndex;
    }
}
