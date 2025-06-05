using System.Collections.Generic;

namespace dboard.Constants;

public static class WorkspaceConstants
{
    public const string WORKSPACE_IMAGE_NAME = "Workspace Image";
    public const string WINDOW_IMAGE_NAME = "Window Image";
    public const string CANVAS_IMAGE_NAME = "Canvas Image";

    public const double MIN_ZOOM = 0.2;
    public const double MAX_ZOOM = 3.0;

    public const double DEFAULT_PANE_LENGTH = 250;

    public enum SAVE_STATUS
    {
        UNSAVED,
        SAVING,
        SAVED
    }

    public enum HISTORY_ACTION
    {
        UNDO,
        REDO
    }

    public static readonly Dictionary<SAVE_STATUS, string> save_status_text = new Dictionary<SAVE_STATUS, string>
    {
        { SAVE_STATUS.UNSAVED, "Unsaved" },
        { SAVE_STATUS.SAVING, "Saving" },
        { SAVE_STATUS.SAVED, "Saved" },
    };

    public enum SAVE_DIALOG_ACTION
    {
        CLOSE_WINDOW,
        OPEN_WORKSPACE
    }
}
