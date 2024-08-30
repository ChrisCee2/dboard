using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Immutable;

namespace mystery_app.Constants;

public static class EdgeConstants
{
    public const double DEFAULT_THICKNESS = 2;
    public static readonly ImmutableSolidColorBrush DEFAULT_COLOR = new ImmutableSolidColorBrush(new Color(255, 50, 50, 50));

    public const double EDGE_TO_NODE_DISTANCE = 20;
    public const double EDGE_PIN_SIZE = 12;
    public static readonly CornerRadius EDGE_PIN_RAD = new CornerRadius(12);
}
