using System.Collections.Generic;
using Avalonia;
using dboard.ViewModels;

namespace dboard.Constants;

public static class NodeConstants
{
    public static readonly NodeViewModel NULL_NODEVIEWMODEL = new NodeViewModel();

    public const string DEFAULT_IMAGE_PATH = "avares://dboard/Assets/amongusbutt.png";

    // Size constants
    public const int DEFAULT_WIDTH = 140;
    public const int DEFAULT_HEIGHT = 160;

    public const int DEFAULT_MIN_WIDTH = 140;
    public const int DEFAULT_MIN_HEIGHT = 160; // Extra 20 to account for edge

    public const int EDGE_HEIGHT = 20; // Extra 20 to account for edge
    public const int IMAGE_MIN_HEIGHT = 40;

    // Resize constants
    public enum RESIZE
    {
        X, // RIGHT
        Y, // DOWN
        XY,// RIGHT DOWN
        x, // LEFT
        y, // UP
        xY,// LEFT DOWN
        Xy,// RIGHT UP
        xy // LEFT UP
    }
    public static readonly Dictionary<RESIZE, Point> RESIZE_TO_DIR = new Dictionary<RESIZE, Point>()
    {
        { RESIZE.X, new Point( 1, 0) },
        { RESIZE.Y, new Point( 0, 1) },
        {RESIZE.XY, new Point( 1, 1) },
        { RESIZE.x, new Point(-1, 0) },
        { RESIZE.y, new Point( 0,-1) },
        {RESIZE.xY, new Point(-1, 1) },
        {RESIZE.Xy, new Point( 1,-1) },
        {RESIZE.xy, new Point(-1,-1) }
    };
}
