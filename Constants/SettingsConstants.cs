using System.Collections.ObjectModel;
using Avalonia.Media;

namespace mystery_app.Constants;

public static class SettingsConstants
{
    public static Color DEFAULT_BACKGROUND_COLOR => new Color(255, 255, 255, 255);
    public static string DEFAULT_THEME => "Default";
    public static Collection<string> THEMES => new Collection<string>() {"Default", "Light", "Dark"};
}
