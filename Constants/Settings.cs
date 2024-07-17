using System.Collections.ObjectModel;

namespace mystery_app.Constants;

public static class Settings
{
    public static string DEFAULT_BACKGROUND_COLOR => "#FFFFFF";
    public static string DEFAULT_THEME => "Default";
    public static Collection<string> THEMES = new Collection<string>() {"Default", "Light", "Dark"};
}
