namespace FileStorage.Application.Interfaces;

public interface IFileProvider
{
    public void SaveFile(Guid id, byte[] content, string originalName, DateTime uploadedAt);
    public byte[] GetFile(Guid id, string originalName, DateTime uploadedAt);
    public void DeleteFile(Guid id, string originalName, DateTime uploadedAt);
}
