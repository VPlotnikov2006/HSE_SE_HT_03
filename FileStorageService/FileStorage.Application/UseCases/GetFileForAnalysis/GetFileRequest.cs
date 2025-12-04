namespace FileStorage.Application.UseCases.GetFileForAnalysis;

public class GetFileRequest(Guid fileId)
{
    public Guid FileId { get; private set; } = fileId;
}
