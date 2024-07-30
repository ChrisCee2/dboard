using System.Net.Http;
using System.Text.RegularExpressions;

namespace mystery_app.Constants;

public static class ImageConstants
{
    public static HttpClient httpClient = new();
    public static Regex imageUrlRegex = new Regex(@"(jpg|jpeg|png|webp|avif|gif|bmp)$");
}
