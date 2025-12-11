using Gateway.Application.DTOs.FileAnalysisDTOs.AnalyseFile;
using Gateway.Application.DTOs.FileStorageDTOs;
using Gateway.Application.Interfaces.Clients;

namespace Gateway.Application.UseCases;

public class SaveFileUseCase(IFileStorageClient storageClient, IFileAnalysisClient analysisClient)
{
    private readonly IFileAnalysisClient _analysisClient = analysisClient;
    private readonly IFileStorageClient _storageClient = storageClient;

    public SaveFileResponse Execute(SaveFileRequest request)
    {
        var response = _storageClient.SaveFile(request);

        _analysisClient.AnalyseFile(new AnalyseFileRequest(request.WorkId, response.FileId));

        return response;
    }
}
