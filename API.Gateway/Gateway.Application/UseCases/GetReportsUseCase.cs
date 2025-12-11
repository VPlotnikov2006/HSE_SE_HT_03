using Gateway.Application.DTOs.FileAnalysisDTOs.GetReports;
using Gateway.Application.Interfaces.Clients;

namespace Gateway.Application.UseCases;

public class GetReportsUseCase(IFileAnalysisClient analysisClient)
{
    private readonly IFileAnalysisClient _analysisClient = analysisClient;

    public IReadOnlyCollection<GetReportsResponse> Execute(GetReportsRequest request)
    {
        return _analysisClient.GetReports(request);
    }
}
