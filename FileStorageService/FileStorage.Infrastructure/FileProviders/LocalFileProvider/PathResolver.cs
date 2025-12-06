namespace FileStorage.Infrastructure.FileProviders.LocalFileProvider;

public static class PathResolver
{
    public static string GetRelativeFilePath(Guid fileId, string originalName, DateTime uploadedAt)
    {
        string safeName = Path.GetFileNameWithoutExtension(originalName);
        string ext = Path.GetExtension(originalName)?.TrimStart('.') ?? "";

        string year = uploadedAt.Year.ToString("D4");
        string month = uploadedAt.Month.ToString("D2");
        string day = uploadedAt.Day.ToString("D2");

        string finalName = ext.Length > 0
            ? $"{fileId}_{safeName}.{ext}"
            : $"{fileId}_{safeName}";

        return Path.Combine(year, month, day, finalName);
    }
}
