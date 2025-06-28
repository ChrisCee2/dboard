using Avalonia;
using dboard.Constants;

namespace dboard.Tools;

class EdgeTools
{

    // Get point where edge should appear given a node's coordinates and width
    public static Point EdgePosFromNode(double x, double y, double width, double endX = 0, double endY = 0)
    {
        Vector offset = new Vector(0, 0);
        if (endX != 0 && endY != 0)
        {
            offset = new Vector(endX - x, endY - y).Normalize();
        }
        return new Point(x + (width / 2), y + EdgeConstants.distFromEdgeToNodePos) + new Point(offset.X, offset.Y) * 5;
    }
}