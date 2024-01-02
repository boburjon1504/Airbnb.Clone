namespace AirBnb.Domain.Extensions;

public static class FileExtensions
{
    public static string ToUrl(this string path, string? prefix)
    {
        return $"{prefix + "/"}{path.Replace("\\", "/")}";
    }
}