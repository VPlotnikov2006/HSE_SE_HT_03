using System.Net.Http.Json;
using FileAnalysis.Application.DTOs;
using FileAnalysis.Application.Interfaces;

namespace FileAnalysis.Infrastructure.Clients;

public class FileStorageClient(HttpClient http) : IFileStorageClient
{
    private readonly HttpClient _http = http;

    public IReadOnlyCollection<Guid> GetFilesIdByWorkId(Guid workId)
    {
        var response = _http
            .GetFromJsonAsync<GetByWorkIdResponse>(
                $"internal/files/by-work/{workId}")
            .GetAwaiter()
            .GetResult() ?? throw new InvalidOperationException("Empty response from FileStorage");
        return response.FileIds;
    }

    public FileContentDto GetFileContent(Guid fileId)
    {
        var response = _http
            .GetFromJsonAsync<GetFileResponse>(
                $"internal/files/{fileId}")
            .GetAwaiter()
            .GetResult() ?? throw new InvalidOperationException("Empty response from FileStorage");
        return new FileContentDto(
            response.Metadata,
            response.Content
        );
    }
}