using Avalonia.Media;
using Avalonia.Media.Immutable;

namespace dboard.Constants;

public static class EdgeConstants
{
    public const double DEFAULT_THICKNESS = 2;
    public static readonly ImmutableSolidColorBrush DEFAULT_COLOR = new ImmutableSolidColorBrush(new Color(255, 50, 50, 50));

    public const double distFromEdgeToNodePos = 11;
}
