using System.Collections.Generic;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using mystery_app.ViewModels;

namespace mystery_app.Constants;

public static class NodeConstants
{
    public static readonly NodeViewModel NULL_NODEVIEWMODEL = new NodeViewModel();

    public const string DEFAULT_IMAGE_PATH = "avares://mystery_app/Assets/amongusbutt.png";

    // Node Color constants
    public static readonly IBrush TOGGLED_COLOR = new ImmutableSolidColorBrush(new Color(255, 165, 140, 100));
    public static readonly IBrush UNTOGGLED_COLOR = new ImmutableSolidColorBrush(new Color(255, 200, 185, 140));
    public static readonly IBrush TEXTBOX_BORDER_COLOR = new ImmutableSolidColorBrush(new Color(255, 200, 185, 140));

    // Size constants
    public const int MIN_WIDTH = 140;
    public const int MIN_HEIGHT = 160; // Extra 20 to account for edge

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
