namespace FileAnalysis.Application.DTOs;

public record FileMetadataDto(
    Guid FileId,
    Guid WorkId,
    string OriginalName,
    string Owner,
    long Size,
    string Checksum,
    DateTime UploadedAt
);