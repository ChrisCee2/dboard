using System.Collections.ObjectModel;
using mystery_app.ViewModels;

namespace mystery_app.Constants;

public static class NodeConstants
{
    public static int MIN_WIDTH => 100;
    public static int MIN_HEIGHT => 100;
    public static string EDGE_BUTTON_TAG => "EdgeButton";
    public static string NONMOVABLE_TAG => "Nonmovable";
    public static Collection<string> NONMOVABLE_TAGS => new Collection<string>() { EDGE_BUTTON_TAG, NONMOVABLE_TAG };
    public static readonly NodeViewModel NULL_NODEVIEWMODEL = new NodeViewModel(); 
}
