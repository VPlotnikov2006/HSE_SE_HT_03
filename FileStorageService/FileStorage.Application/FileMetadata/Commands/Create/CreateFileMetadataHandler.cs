using FileStorage.Application.Interfaces;

namespace FileStorage.Application.FileMetadata.Commands.Create;

public class CreateFileMetadataHandler(IFileRepository repository)
{
    private readonly IFileRepository _repository = repository;

    public Guid Handle(CreateFileMetadata request)
    {
        
    }

    private string SaveFile(byte[] content, string fileName)
    {
        
    }
}
