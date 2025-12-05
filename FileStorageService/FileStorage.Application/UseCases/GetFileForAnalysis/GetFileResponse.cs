using FileStorage.Domain;

namespace FileStorage.Application.UseCases.GetFileForAnalysis;

public record GetFileResponse(FileMetadata Metadata, byte[] Content)
{
}
