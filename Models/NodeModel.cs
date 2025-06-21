using CommunityToolkit.Mvvm.ComponentModel;
using dboard.Constants;

namespace dboard.Models;

public partial class NodeModel : NodeModelBase
{
    private int defaultMinHeight = 60;
    private int defaultMinWidth = 100;

    public NodeModel()
    {
        Name = "";
        Desc = "";
        ImagePath = null;
        MinWidth = defaultMinWidth;
        MinHeight = defaultMinHeight + NodeConstants.EDGE_HEIGHT;
        Width = MinWidth;
        Height = MinHeight;
        PositionX = 0;
        PositionY = 0;
        Notes = "";
        ZIndex = 0;
        A = 255;
        R = 255;
        G = 255;
        B = 255;
    }

    public NodeModel(int zIndex)
    {
        Name = "";
        Desc = "";
        ImagePath = null;
        MinWidth = defaultMinWidth;
        MinHeight = defaultMinHeight + NodeConstants.EDGE_HEIGHT;
        Width = MinWidth;
        Height = MinHeight;
        PositionX = 0;
        PositionY = 0;
        Notes = "";
        ZIndex = zIndex;
        A = 255;
        R = 255;
        G = 255;
        B = 255;
    }

    public NodeModel(
        string name,
        string desc,
        string imagePath,
        double minWidth,
        double minHeight,
        double width,
        double height,
        double x,
        double y,
        string notes,
        int zIndex,
        byte a,
        byte r,
        byte g,
        byte b)
    {
        Name = name;
        Desc = desc;
        ImagePath = imagePath;
        MinWidth = minWidth;
        MinHeight = minHeight;
        Width = width;
        Height = height;
        PositionX = x;
        PositionY = y;
        Notes = notes;
        ZIndex = zIndex;
        A = a;
        R = r;
        G = g;
        B = b;
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
            MinWidth,
            MinHeight,
            Width,
            Height,
            PositionX,
            PositionY,
            Notes,
            ZIndex,
            A,
            R,
            G,
            B
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
        MinWidth = nodeModel.MinWidth;
        MinHeight = nodeModel.MinHeight;
        PositionX = nodeModel.PositionX;
        PositionY = nodeModel.PositionY;
        Notes = nodeModel.Notes;
        ZIndex = nodeModel.ZIndex;
        A = nodeModel.A;
        R = nodeModel.R;
        G = nodeModel.G;
        B = nodeModel.B;
    }

    partial void OnImagePathChanged(string? value)
    {
        HandleResize();
    }

    partial void OnNotesToggledChanged(bool value)
    {
        HandleResize();
    }

    private void HandleResize()
    {
        double finalMinHeight = defaultMinHeight;

        if (NotesToggled)
        {
            finalMinHeight += defaultMinHeight;
        }

        if (ImagePath is not null)
        {
            finalMinHeight = finalMinHeight + NodeConstants.IMAGE_MIN_HEIGHT;
        }

        MinHeight = finalMinHeight + NodeConstants.EDGE_HEIGHT;
        if (Height < MinHeight)
        {
            Height = MinHeight;
        }
    }
}
