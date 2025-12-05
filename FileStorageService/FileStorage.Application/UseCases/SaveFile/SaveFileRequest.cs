namespace FileStorage.Application.UseCases.SaveFile;

public record SaveFileRequest(string OriginalName, byte[] Content, string Owner, Guid WorkId)
{
}
