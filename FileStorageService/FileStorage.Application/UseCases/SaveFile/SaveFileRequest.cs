namespace FileStorage.Application.UseCases.SaveFile;

public class SaveFileRequest
{
    public required string OriginalName { get; init; }
    public required byte[] Content { get; init; }
    public required string Owner { get; init; }
    public required Guid WorkId { get; init; }
}
