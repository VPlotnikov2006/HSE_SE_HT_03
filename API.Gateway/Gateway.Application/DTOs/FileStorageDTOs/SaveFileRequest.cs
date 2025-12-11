namespace Gateway.Application.DTOs.FileStorageDTOs;

public record SaveFileRequest(string OriginalName, byte[] Content, string Owner, Guid WorkId)
{
}
