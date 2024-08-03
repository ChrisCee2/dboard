using System.Collections.Generic;
using Avalonia;
using mystery_app.ViewModels;

namespace mystery_app.Constants;

public static class NodeConstants
{
    public static readonly NodeViewModel NULL_NODEVIEWMODEL = new NodeViewModel();

    // Size constants
    public const int MIN_WIDTH = 100;
    public const int MIN_HEIGHT = 120; // Extra 20 to account for edge

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
