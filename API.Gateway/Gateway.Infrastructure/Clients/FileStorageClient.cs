using System.Net.Http.Json;
using Gateway.Application.DTOs.FileStorageDTOs;
using Gateway.Application.Interfaces.Clients;

namespace Gateway.Infrastructure.Clients;

public class FileStorageClient(HttpClient http) : IFileStorageClient
{
    private readonly HttpClient _http = http;

    public SaveFileResponse SaveFile(SaveFileRequest request)
    {
        var response = _http
            .PostAsJsonAsync("api/files", request)
            .Result;

        response.EnsureSuccessStatusCode();

        var data = response
            .Content
            .ReadFromJsonAsync<SaveFileResponse>()
            .Result ?? throw new InvalidOperationException("Invalid SaveFileResponse payload.");
        return data;
    }
}

