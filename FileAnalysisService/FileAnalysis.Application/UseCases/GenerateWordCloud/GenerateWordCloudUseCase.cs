using FileAnalysis.Application.Interfaces;

namespace FileAnalysis.Application.UseCases.GenerateWordCloud;

public class GenerateWordCloudUseCase(IFileStorageClient storageClient, IWordCloudClient wordCloudClient)
{
    private readonly IFileStorageClient _storage = storageClient;
    private readonly IWordCloudClient _cloud = wordCloudClient;

    public GenerateWordCloudResponse Execute(GenerateWordCloudRequest request)
    {
        var file = _storage.GetFileContent(request.FileId);

        string text = System.Text.Encoding.UTF8.GetString(file.Content);

        byte[] image = _cloud.BuildWordCloud(text);

        return new GenerateWordCloudResponse(image);
    }
}
