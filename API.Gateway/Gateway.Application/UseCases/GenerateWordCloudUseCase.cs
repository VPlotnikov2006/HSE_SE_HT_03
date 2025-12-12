using Gateway.Application.DTOs.FileAnalysisDTOs.GenerateWordCloud;
using Gateway.Application.Interfaces.Clients;

namespace Gateway.Application.UseCases;

public class GenerateWordCloudUseCase(IFileAnalysisClient analysisClient)
{
    private readonly IFileAnalysisClient _analysisClient = analysisClient;

    public GenerateWordCloudResponse Execute(GenerateWordCloudRequest request)
    {
        return _analysisClient.GenerateWordCloud(request);
    }
}
