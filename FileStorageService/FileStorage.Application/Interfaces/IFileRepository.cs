using FileStorage.Domain;

namespace FileStorage.Application.Interfaces;

public interface IFileRepository
{
    FileMetadata? GetById(Guid fileId);
    IReadOnlyCollection<Guid> GetFileIds(Guid workId);
    void Add(FileMetadata metadata);

    public void SaveChanges();
}
