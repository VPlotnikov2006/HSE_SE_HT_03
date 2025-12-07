using FileStorage.Application.Interfaces;

namespace FileStorage.Application.UseCases.GetAllFilesForAnalysis;

public class GetByWorkIdUseCase(IFileProvider fileProvider, IFileRepository fileRepository)
{
    private readonly IFileProvider _fileProvider = fileProvider;
    private readonly IFileRepository _fileRepository = fileRepository;

    public GetByWorkIdResponse Execute(GetByWorkIdRequest request)
    {
        return new([.. _fileRepository.GetFileIds(request.WorkId)]);
    }
}
