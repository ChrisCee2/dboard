using System.Collections.ObjectModel;

namespace mystery_app.Constants;

public static class Node
{
    public static int EDGE_THICKNESS => 2;
    public static string EDGE_BUTTON_TAG => "EdgeButton";
    public static string NONMOVABLE_TAG => "Nonmovable";
    public static Collection<string> NONMOVABLE_TAGS => new Collection<string>() { EDGE_BUTTON_TAG, NONMOVABLE_TAG };
}
