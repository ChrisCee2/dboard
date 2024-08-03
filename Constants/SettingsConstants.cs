using System.Collections.ObjectModel;
using Avalonia.Media;
using mystery_app.Models;

namespace mystery_app.Constants;

public static class SettingsConstants
{
    // Theme Constants
    public static readonly Collection<string> THEMES = new Collection<string>() { "Default", "Light", "Dark" };
    public static readonly Color DEFAULT_BACKGROUND_COLOR = new Color(255, 255, 255, 255);

    // Mode Constants
    public static readonly Color TRANSPARENT_BACKGROUND_COLOR = new Color(0, 0, 0, 0);
    public const double TRANSPARENT_ITEM_OPACITY = 0;
    public const double TRANSPARENT_WORKSPACE_OPACITY = 0.5;
    public const string TRANSPARENT_WINDOW_STATE = "FullScreen";

    public static readonly ModeModel DEFAULT_MODE = new ModeModel("Default", DEFAULT_BACKGROUND_COLOR, true, 1, "Normal");
    public static readonly ModeModel TRANSPARENT_MODE = new ToggleModeModel("Transparent", TRANSPARENT_BACKGROUND_COLOR, false, TRANSPARENT_WORKSPACE_OPACITY,TRANSPARENT_WINDOW_STATE);
    public static readonly Collection<ModeModel> MODES = new Collection<ModeModel>()
    {
        TRANSPARENT_MODE
    };
}
