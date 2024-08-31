using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Media;
using mystery_app.Models;

namespace mystery_app.Constants;

public static class SettingsConstants
{

    // Theme Constants
    public static readonly Collection<string> THEMES = new Collection<string>() { "Default", "Light", "Dark" };

    public static readonly Color DEFAULT_COLOR = new Color(255, 255, 255, 255);
    public static readonly Color DEFAULT_ACCENT_COLOR = new Color(100, 150, 150, 150);
    public static readonly Color TRANSPARENT_COLOR = new Color(0, 0, 0, 0);
    public static readonly ModeModel DEFAULT_MODE = new ModeModel("Default", true, 1, "Normal", DEFAULT_COLOR, DEFAULT_ACCENT_COLOR);
    public static readonly ModeModel TRANSPARENT_MODE = new ModeModel("Transparent", false, 0.5, "FullScreen", TRANSPARENT_COLOR, DEFAULT_ACCENT_COLOR);
    public static readonly List<ModeModel> AVAILABLE_MODES = new List<ModeModel>()
    {
        new ModeModelToggle("Transparent", TRANSPARENT_MODE, DEFAULT_MODE)
    };
}
