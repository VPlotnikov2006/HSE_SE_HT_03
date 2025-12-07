namespace FileStorage.Application.UseCases.GetFileForAnalysis;

public record GetFileRequest()
{
    public Guid FileId { get; init; }
}
