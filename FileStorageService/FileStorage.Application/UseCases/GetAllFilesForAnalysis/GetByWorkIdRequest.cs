namespace FileStorage.Application.UseCases.GetAllFilesForAnalysis;

public record GetByWorkIdRequest()
{
    public Guid WorkId { get; init; }
}
