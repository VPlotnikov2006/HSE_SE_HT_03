using FileStorage.Domain;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.Application.Interfaces;

// TODO убрать DbSet наружу (в Infrastructure)
public interface IFileRepository
{
    DbSet<FileMetadata> Files {get; set; }

    public bool SaveChanges();
}
