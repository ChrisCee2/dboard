using System.Collections.ObjectModel;
using Avalonia.Media;
using mystery_app.Models;

namespace mystery_app.Constants;

public static class SettingsConstants
{
    public static Color DEFAULT_BACKGROUND_COLOR => new Color(255, 255, 255, 255);

    // Transparent settings
    public static Color TRANSPARENT_BACKGROUND_COLOR => new Color(0, 0, 0, 0);
    public static double TRANSPARENT_ITEM_OPACITY => 0;
    public static double TRANSPARENT_WORKSPACE_OPACITY => 0.5;
    public static string TRANSPARENT_WINDOW_STATE => "FullScreen";


    public static string DEFAULT_THEME => "Default";
    public static ModeModel DEFAULT_MODE => new ModeModel("Default", DEFAULT_BACKGROUND_COLOR, 1, 1, "Normal");
    public static ModeModel TRANSPARENT_MODE => new ToggleModeModel(
        "Transparent", 
        TRANSPARENT_BACKGROUND_COLOR, 
        TRANSPARENT_ITEM_OPACITY, 
        TRANSPARENT_WORKSPACE_OPACITY,
        TRANSPARENT_WINDOW_STATE);
    public static Collection<string> THEMES => new Collection<string>() { "Default", "Light", "Dark" };
    public static Collection<ModeModel> MODES = new Collection<ModeModel>()
    {
        TRANSPARENT_MODE
    };
    // TODO: Figure out whether it's better to do this and readonly OR use the const type. Const type is only compile time tho
}
