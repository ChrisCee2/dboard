using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using mystery_app.Models;

namespace mystery_app.Constants;

public static class SettingsConstants
{
    // Constants for settings view
    public static readonly ImmutableSolidColorBrush COLOR = new ImmutableSolidColorBrush(new Color(50, 255, 255, 255));

    // Theme Constants
    public static readonly Collection<string> THEMES = new Collection<string>() { "Default", "Light", "Dark" };
    public static readonly Color DEF_CLR = new Color(255, 255, 255, 255);

    // Mode Constants
    public static readonly Color T_CLR = new Color(0, 0, 0, 0);
    public const double TRANSPARENT_WORKSPACE_OPACITY = 0.5;
    public const string TRANSPARENT_WINDOW_STATE = "FullScreen";

    public static readonly ModeModel DEFAULT_MODE = new ModeModel("Default", true, 1, "Normal", DEF_CLR.A, DEF_CLR.R, DEF_CLR.G, DEF_CLR.B);
    public static readonly ModeModel TRANSPARENT_MODE = new ModeModel("Transparent", false, TRANSPARENT_WORKSPACE_OPACITY, TRANSPARENT_WINDOW_STATE, T_CLR.A, T_CLR.R, T_CLR.G, T_CLR.B);
    public static readonly List<ModeModel> AVAILABLE_MODES = new List<ModeModel>()
    {
        new ModeModelToggle("Transparent", TRANSPARENT_MODE, DEFAULT_MODE)
    };
}
