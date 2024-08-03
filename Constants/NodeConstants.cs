using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;
using mystery_app.ViewModels;

namespace mystery_app.Constants;

public static class NodeConstants
{
    public static int MIN_WIDTH => 100;
    public static int MIN_HEIGHT => 120; // Extra 20 to account for edge
    public static string EDGE_BUTTON_TAG => "EdgeButton";
    public static string NONMOVABLE_TAG => "Nonmovable";
    public static Collection<string> NONMOVABLE_TAGS => new Collection<string>() { EDGE_BUTTON_TAG, NONMOVABLE_TAG };
    public static readonly NodeViewModel NULL_NODEVIEWMODEL = new NodeViewModel(); 
    public enum ResizeAxis
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
    public static Dictionary<ResizeAxis, Point> AxisToDir => new Dictionary<ResizeAxis, Point>()
    {
        { ResizeAxis.X, new Point( 1, 0) },
        { ResizeAxis.Y, new Point( 0, 1) },
        {ResizeAxis.XY, new Point( 1, 1) },
        { ResizeAxis.x, new Point(-1, 0) },
        { ResizeAxis.y, new Point( 0,-1) },
        {ResizeAxis.xY, new Point(-1, 1) },
        {ResizeAxis.Xy, new Point( 1,-1) },
        {ResizeAxis.xy, new Point(-1,-1) }
    };
}
