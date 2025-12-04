namespace FileStorage.Application.UseCases.SaveFile;

public class SaveFileResponse(Guid fileId)
{
    public Guid FileId { get; init; } = fileId;
}
