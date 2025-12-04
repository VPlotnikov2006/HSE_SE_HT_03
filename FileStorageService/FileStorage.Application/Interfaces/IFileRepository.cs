using System;
using FileStorage.Domain;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.Application.Interfaces;

public interface IFileRepository
{
    DbSet<Domain.FileMetadata> Files {get; set; }

    public bool SaveChanges();
}
