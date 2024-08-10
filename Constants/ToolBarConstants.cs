using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Immutable;

namespace mystery_app.Constants;

public static class ToolBarConstants
{
    public static readonly ImmutableSolidColorBrush COLOR = new ImmutableSolidColorBrush(new Color(170, 150, 150, 150));
    public static readonly ImmutableSolidColorBrush BUTTON_BACKGROUND = new ImmutableSolidColorBrush(new Color(0, 0, 0, 0));
    public static readonly Thickness WORKSPACE_NORM_PAD = new Thickness(0, 0, 0, 0);
    public static readonly Thickness WORKSPACE_MAX_PAD = new Thickness(8, 0, 8, 0);
    public static readonly Thickness MAX_MBTN_PAD = new Thickness(12, 16, 12, 8);
    public static readonly Thickness NORM_MBTN_PAD = new Thickness(12, 8, 12, 8);
    public static readonly Thickness MAX_LPAD = new Thickness(20, 16, 12, 8);
    public static readonly Thickness NORM_LPAD = new Thickness(12, 8, 12, 8);
    public static readonly Thickness MAX_RBTN_PAD = new Thickness(12, 16, 20, 8);
    public static readonly Thickness NORM_RBTN_PAD = new Thickness(12, 8, 12, 8);
}
