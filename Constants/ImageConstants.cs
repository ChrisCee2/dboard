using System.Text.RegularExpressions;

namespace mystery_app.Constants;

public static class ImageConstants
{
    public static readonly Regex IMAGE_URL_REGEX = new Regex(@"(jpg|jpeg|png|webp|avif|gif|bmp)$");
}
