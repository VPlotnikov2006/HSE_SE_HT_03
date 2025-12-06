using FileStorage.Application.Interfaces;
using FileStorage.Domain;

namespace FileStorage.Infrastructure.Persistence;

public class EfFileMetadataRepository(FileStorageDbContext db) : IFileRepository
{
    private readonly FileStorageDbContext _db = db;

    public void Add(FileMetadata metadata)
    {
        _db.Files.Add(metadata);
    }

    public FileMetadata? GetById(Guid fileId)
    {
        return _db.Files.Find(fileId);
    }

    public IReadOnlyCollection<Guid> GetFileIds(Guid workId)
    {
        return [.. _db.Files.Where(f => f.WorkId == workId).Select(f => f.FileId)];
    }

    public void SaveChanges()
    {
        _db.SaveChanges();
    }
}
