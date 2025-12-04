using FileStorage.Application.Interfaces;

namespace FileStorage.Application.UseCases.GetFileForAnalysis;

public class GetFileUseCase(IFileProvider fileProvider, IFileRepository fileRepository)
{
    private readonly IFileProvider _fileProvider = fileProvider;
    private readonly IFileRepository _fileRepository = fileRepository;

    public GetFileResponse Execute(GetFileRequest request)
    {
        var fileId = request.FileId;
        var metadata = _fileRepository.Files.Find(request.FileId) 
            ?? throw new FileNotFoundException($"File {fileId} metadata not found.");

        var content = _fileProvider.GetFile(fileId, metadata.OriginalName, metadata.UploadedAt);

        if (content.Length != metadata.Size)
        {
            throw new InvalidOperationException(
                $"File size mismatch: real={content.Length}, meta={metadata.Size}");
        }

        return new GetFileResponse(
            metadata,
            content
        );
    }
}
