namespace FileAnalysis.Application.DTOs;

public record FileContentDto(FileMetadataDto Metadata, byte[] Content)
{
}
