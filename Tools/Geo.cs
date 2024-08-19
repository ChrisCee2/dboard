using Avalonia;

namespace mystery_app.Tools;

class Geo
{

    // Lines defined by 2 points, 4 points total for lines a and b
    public static bool LineInLine(Point a0, Point a1, Point b0, Point b1)
    {
        double determinant = ((a1.X - a0.X) * (b1.Y - b0.Y)) - ((b1.X - b0.X) * (a1.Y - a0.Y));
        if ( determinant == 0 ) { return false; }
        
        double uA = (((b1.X - b0.X) * (a0.Y - b0.Y)) - ((b1.Y - b0.Y) * (a0.X - b0.X))) / determinant;
        double uB = (((a1.X - a0.X) * (a0.Y - b0.Y)) - ((a1.Y - a0.Y) * (a0.X - b0.X))) / determinant;
        if (uA >= 0 && uA <= 1 && uB >= 0 && uB <= 1) { return true; }
        
       return false;
    }

    // Rectangles defined by 2 points, 4 points total for rectangles a and b
    public static bool RectInRect(Point a0, Point a1, Point b0, Point b1)
    {
        if (   a0.X < b1.X // Rectangle a's left side is before b's right side, otherwise a is to the right of b
            && a1.X > b0.X // Rectangle a's right side is after b's left side, otherwise a is to the left of b
            && a0.Y < b1.Y // Rectangle a's bottom side is lower than b's top side, otherwise a is above b
            && a1.Y > b0.Y // Rectangle a's top side is higher than b's bottom side, otherwise b is above a
           )
        {  return true; }
        return false;
    }

    // Lines defined by 2 points, 4 points total for lines a and b
    public static bool LineInRect(Point line0, Point line1, Point rect0, Point rect1)
    {
        bool intersectLeft = LineInLine(line0, line1, new Point(rect0.X, rect0.Y), new Point(rect0.X, rect1.Y));
        bool intersectTop = LineInLine(line0, line1, new Point(rect0.X, rect0.Y), new Point(rect1.X, rect0.Y));
        bool intersectRight = LineInLine(line0, line1, new Point(rect1.X, rect0.Y), new Point(rect1.X, rect1.Y));
        bool intersectBot = LineInLine(line0, line1, new Point(rect0.X, rect1.Y), new Point(rect1.X, rect1.Y));
        if (intersectLeft || intersectTop || intersectRight || intersectBot) { return true; }
        
        // If start of line is within the rectangle and all the other checks failed, it is completely inside the rectangle
        if (rect0.X < line0.X && line0.X < rect1.X && rect0.Y < line0.Y && line0.Y < rect1.Y) { return true; }

        return false;
    }
}