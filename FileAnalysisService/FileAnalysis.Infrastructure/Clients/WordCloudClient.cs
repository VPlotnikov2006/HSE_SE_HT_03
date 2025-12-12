using System.Net.Http.Json;
using FileAnalysis.Application.Interfaces;

namespace FileAnalysis.Infrastructure.Clients;

public class WordCloudClient(HttpClient http) : IWordCloudClient
{
    private readonly HttpClient _http = http;

    private record WordCloudRequest(
        string Format,
        int Width,
        int Height,
        int FontScale,
        string Scale,
        int MinWordLength,
        string Text
    );

    public byte[] BuildWordCloud(string text)
    {
        var body = new WordCloudRequest(
            Format: "png",
            Width: 1000,
            Height: 1000,
            FontScale: 15,
            Scale: "linear",
            MinWordLength: 4,
            Text: text
        );

        var response = _http.PostAsJsonAsync("", body).Result;

        response.EnsureSuccessStatusCode();

        return response.Content.ReadAsByteArrayAsync().Result;
    }
}