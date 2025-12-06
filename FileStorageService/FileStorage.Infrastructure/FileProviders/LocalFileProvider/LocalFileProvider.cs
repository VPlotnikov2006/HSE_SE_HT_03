using FileStorage.Application.Interfaces;
using FileStorage.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace FileStorage.Infrastructure.FileProviders.LocalFileProvider;

public class LocalFileProvider(IOptions<FileStorageOptions> opts) : IFileProvider
{
    private readonly FileStorageOptions _opts = opts.Value;

    public void DeleteFile(Guid id, string originalName, DateTime uploadedAt)
    {
        string relativePath = PathResolver.GetRelativeFilePath(id, originalName, uploadedAt);
        string fullPath = Path.Combine(_opts.RootPath, relativePath);

        if (File.Exists(fullPath))
            File.Delete(fullPath);
    }

    public byte[] GetFile(Guid id, string originalName, DateTime uploadedAt)
    {
        string relativePath = PathResolver.GetRelativeFilePath(id, originalName, uploadedAt);
        string fullPath = Path.Combine(_opts.RootPath, relativePath);

        if (!File.Exists(fullPath))
            throw new FileNotFoundException("File not found", fullPath);

        return File.ReadAllBytes(fullPath);
    }

    public void SaveFile(Guid id, byte[] content, string originalName, DateTime uploadedAt)
    {
        string relativePath = PathResolver.GetRelativeFilePath(id, originalName, uploadedAt);
        string fullPath = Path.Combine(_opts.RootPath, relativePath);

        Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);

        File.WriteAllBytes(fullPath, content);
    }
}
