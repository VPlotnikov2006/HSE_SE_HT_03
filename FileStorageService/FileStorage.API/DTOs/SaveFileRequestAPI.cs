namespace FileStorage.API.DTOs;

public class SaveFileRequestApi
{
    public string OriginalName { get; set; } = null!;
    public IFormFile Content { get; set; } = null!;
    public string Owner { get; set; } = null!;
    public Guid WorkId { get; set; }
}