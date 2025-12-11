using Gateway.Application.DTOs.FileAnalysisDTOs.AnalyseFile;
using Gateway.Application.DTOs.FileAnalysisDTOs.GetReports;

namespace Gateway.Application.Interfaces.Clients;

public interface IFileAnalysisClient
{
    AnalyseFileResponse AnalyseFile(AnalyseFileRequest request);
    IReadOnlyCollection<GetReportsResponse> GetReports(GetReportsRequest request);
}
