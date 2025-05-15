using System.Text.RegularExpressions;

namespace dboard.Constants;

public static class ImageConstants
{
    public static readonly Regex IMAGE_URL_REGEX = new Regex(@"(?i)(jpg|jpeg|png|webp|avif|gif|bmp)$");
}
