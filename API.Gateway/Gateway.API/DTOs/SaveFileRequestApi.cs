namespace Gateway.API.DTOs;

public record SaveFileRequestApi(string OriginalName, IFormFile Content, string Owner, Guid WorkId)
{
}
