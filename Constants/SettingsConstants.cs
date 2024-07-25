using System.Collections.ObjectModel;
using Avalonia.Media;
using mystery_app.Models;

namespace mystery_app.Constants;

public static class SettingsConstants
{
    public static Color DEFAULT_BACKGROUND_COLOR => new Color(255, 255, 255, 255);
    public static Color TRANSPARENT_BACKGROUND_COLOR => new Color(0, 0, 0, 0);
    public static string DEFAULT_THEME => "Default";
    public static ModeModel DEFAULT_MODE => new ModeModel("Default", DEFAULT_BACKGROUND_COLOR, 1, 1);
    public static Collection<string> THEMES => new Collection<string>() { "Default", "Light", "Dark" };
    public static Collection<ModeModel> MODES = new Collection<ModeModel>()
    {
        { new ModeModel("Transparent", TRANSPARENT_BACKGROUND_COLOR, 0, .5) },
    };
}
