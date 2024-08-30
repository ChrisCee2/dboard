using Avalonia;
using mystery_app.Constants;

namespace mystery_app.Tools;

class EdgeTools
{

    // Get point where edge should appear given a node's coordinates and width
    public static Point EdgePosFromNode(double x, double y, double pinWidth, double nodeWidth)
    {
        return new Point(x + (nodeWidth / 2) - (pinWidth / 2), y - EdgeConstants.EDGE_TO_NODE_DISTANCE);
    }
}