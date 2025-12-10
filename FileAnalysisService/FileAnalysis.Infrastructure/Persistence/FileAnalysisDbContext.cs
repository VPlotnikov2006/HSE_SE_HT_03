using FileAnalysis.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileAnalysis.Infrastructure.Persistence;

public class FileAnalysisDbContext(DbContextOptions<FileAnalysisDbContext> options) : DbContext(options)
{
    public DbSet<PlagiarismReport> Reports => Set<PlagiarismReport>();
    public DbSet<PlagiarismMatch> Matches => Set<PlagiarismMatch>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlagiarismReport>(entity =>
        {
            entity.ToTable("Reports");

            entity.HasKey(r => r.ReportId);

            entity.HasIndex(r => r.FileId).IsUnique();

            entity.Property(r => r.FileId)
                .IsRequired();

            entity.Property(r => r.WorkId)
                .IsRequired();

            entity.Property(r => r.Owner)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(r => r.HighestSimilarity)
                .IsRequired();

            entity.Property(r => r.CheckedAt)
                .IsRequired();

            entity.Navigation(r => r.Matches)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        });

        modelBuilder.Entity<PlagiarismMatch>(entity =>
        {
            entity.ToTable("Matches");

            entity.HasKey(m => m.MatchId);

            entity.HasIndex(m => new { m.ReportId, m.SourceFileId }).IsUnique();

            entity.Property(m => m.SourceFileId)
                .IsRequired();

            entity.Property(m => m.SourceOwner)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(m => m.Similarity)
                .IsRequired();

            entity.Property(m => m.ReportId)
                .IsRequired();

            entity.HasOne<PlagiarismReport>()
                .WithMany("_matches")
                .HasForeignKey(m => m.ReportId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}

