namespace FileStorage.Application.FileMetadata.Commands.Create;

public class CreateFileMetadata
{
    public required byte[]? Content { get; set; }
    public required Guid WorkID { get; set; }
    public required string FileName { get; set; }
    public required string Owner { get; set; }
}
