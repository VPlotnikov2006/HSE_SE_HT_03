using FileStorage.Application.Interfaces;
using FileStorage.Application.Utils;
using FileStorage.Domain;

namespace FileStorage.Application.UseCases.SaveFile;

public class SaveFileUseCase(IFileProvider fileProvider, IFileRepository fileRepository)
{
    private readonly IFileProvider _fileProvider = fileProvider;
    private readonly IFileRepository _fileRepository = fileRepository;


    public SaveFileResponse Execute(SaveFileRequest request)
    {
        var metadata = new FileMetadata(
            request.OriginalName, 
            request.Owner, 
            Checksum.Compute(request.Content), 
            request.Content.Length, 
            request.WorkId
        );

        _fileProvider.SaveFile(
            metadata.FileId, 
            request.Content, 
            metadata.OriginalName, 
            metadata.UploadedAt
        );

        _fileRepository.Files.Add(metadata);
        _fileRepository.SaveChanges();

        return new(metadata.FileId);
    }
}
