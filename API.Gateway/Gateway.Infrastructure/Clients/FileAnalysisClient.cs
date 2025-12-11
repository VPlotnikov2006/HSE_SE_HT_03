using System.Net.Http.Json;
using Gateway.Application.DTOs.FileAnalysisDTOs.AnalyseFile;
using Gateway.Application.DTOs.FileAnalysisDTOs.GetReports;
using Gateway.Application.Interfaces.Clients;

namespace Gateway.Infrastructure.Clients;

public class FileAnalysisClient(HttpClient http) : IFileAnalysisClient
{
    private readonly HttpClient _http = http;

    public AnalyseFileResponse AnalyseFile(AnalyseFileRequest request)
    {
        var response = _http
            .PostAsJsonAsync("internal/analysis", request)
            .Result;

        response.EnsureSuccessStatusCode();

        var data = response
            .Content
            .ReadFromJsonAsync<AnalyseFileResponse>()
            .Result ?? throw new InvalidOperationException("Invalid AnalyseFileResponse payload.");
        return data;
    }

    public GetReportsResponse GetReports(GetReportsRequest request)
    {
        var response = _http
            .GetFromJsonAsync<GetReportsResponse>($"api/reports/by-work/{request.WorkId}")
            .Result ?? throw new InvalidOperationException("Invalid GetReportsResponse payload.");
        return response;
    }
}
