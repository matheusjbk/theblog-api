using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace TheBlog.Domain.Services;

public static partial class SlugGenerator
{
    public static string Generate(string text)
    {
        if(String.IsNullOrWhiteSpace(text)) return string.Empty;

        string slug = RemoveAccents(text).ToLowerInvariant();
        slug = InvalidCharsRegex().Replace(slug, "");

        string suffix = Convert.ToHexString(RandomNumberGenerator.GetBytes(3)).ToLower();

        return $"{MultipleHyphensRegex().Replace(slug, "-").Trim('-')}-{suffix}";
    }

    private static string RemoveAccents(string text)
    {
        var normalizedString = text.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();
        foreach (var c in normalizedString)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                stringBuilder.Append(c);
        }
        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }

    [GeneratedRegex(@"[^a-z0-9\s-]", RegexOptions.Compiled)]
    private static partial Regex InvalidCharsRegex();

    [GeneratedRegex(@"[\s-]+", RegexOptions.Compiled)]
    private static partial Regex MultipleHyphensRegex();
}
