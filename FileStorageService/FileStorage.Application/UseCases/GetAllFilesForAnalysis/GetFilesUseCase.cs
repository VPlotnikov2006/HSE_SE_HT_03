using FileStorage.Application.Interfaces;

namespace FileStorage.Application.UseCases.GetAllFilesForAnalysis;

public class GetFilesUseCase(IFileProvider fileProvider, IFileRepository fileRepository)
{
    private readonly IFileProvider _fileProvider = fileProvider;
    private readonly IFileRepository _fileRepository = fileRepository;

    public GetFilesResponse Execute(GetFilesRequest request)
    {
        return new([.. _fileRepository.GetFileIds(request.WorkId)]);
    }
}
