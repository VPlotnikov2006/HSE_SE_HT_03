using System.Security.Cryptography;
using System.Text;

namespace FileStorage.Domain;

public class FileMetadata
{
    public Guid FileId { get; private set; }
    public string FilePath { get; private set; }
    public long Size { get; private set; }
    public DateTime UploadedAt { get; private set; }
    public string Owner { get; private set; }
    public string Checksum { get; private set; }
    public Guid WorkId { get; private set; }

    public FileMetadata(string filePath, string owner, string checksum, long size, Guid workId)
    {
        if (!Path.Exists(filePath))
        {
            throw new FileNotFoundException();
        }

        ArgumentOutOfRangeException.ThrowIfNegative(size);

        FileId = Guid.NewGuid();
        FilePath = filePath;
        Size = size;
        UploadedAt = DateTime.UtcNow;
        Owner = owner;
        Checksum = checksum;
        WorkId = workId;
    }

    public bool CheckCheckSum()
    {
        if (!Path.Exists(FilePath))
        {
            throw new FileNotFoundException();
        }

        using var md5 = MD5.Create();
        using var stream = File.OpenRead(FilePath);
        return Encoding.Default.GetString(md5.ComputeHash(stream)) == Checksum;
    }
}
