using FileStorage.Domain;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.Infrastructure.Persistence;

public class FileStorageDbContext(DbContextOptions<FileStorageDbContext> options) : DbContext(options)
{
    public DbSet<FileMetadata> Files => Set<FileMetadata>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FileMetadata>(entity =>
        {
            entity.ToTable("Files");

            entity.HasKey(f => f.FileId);

            entity.Property(f => f.FileId)
                .IsRequired();

            entity.Property(f => f.WorkId)
                .IsRequired();

            entity.Property(f => f.OriginalName)
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(f => f.Size)
                .IsRequired();

            entity.Property(f => f.UploadedAt)
                .IsRequired();
            
            entity.Property(f => f.Owner)
                .HasMaxLength(255)
                .IsRequired();
            
            entity.Property(f => f.WorkId)
                .IsRequired();

            entity.Property(f => f.Checksum)
                .HasMaxLength(128)
                .IsRequired();
        });
    }
}
