namespace FileAnalysis.Application.DTOs;

public record FileContentDTO(FileMetadataDto Metadata, byte[] Content)
{
}
