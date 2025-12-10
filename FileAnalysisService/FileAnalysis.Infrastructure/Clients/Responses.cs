using FileAnalysis.Application.DTOs;

namespace FileAnalysis.Infrastructure.Clients;

public record GetByWorkIdResponse(Guid[] FileIds);
public record GetFileResponse(FileMetadataDto Metadata, byte[] Content);