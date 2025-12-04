namespace FileStorage.Domain;

public class FileMetadata
{
    public Guid FileId { get; private set; }
    public string OriginalName { get; private set; }
    public long Size { get; private set; }
    public DateTime UploadedAt { get; private set; }
    public string Owner { get; private set; }
    public string Checksum { get; private set; }
    public Guid WorkId { get; private set; }

    public FileMetadata(string originalName, string owner, string checksum, long size, Guid workId)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(size);

        FileId = Guid.NewGuid();
        OriginalName = originalName;
        Size = size;
        UploadedAt = DateTime.UtcNow;
        Owner = owner;
        Checksum = checksum;
        WorkId = workId;
    }
}
