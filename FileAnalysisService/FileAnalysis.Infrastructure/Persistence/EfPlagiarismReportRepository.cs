using FileAnalysis.Application.Interfaces;
using FileAnalysis.Domain.Entities;

namespace FileAnalysis.Infrastructure.Persistence;

public class EfPlagiarismReportRepository(FileAnalysisDbContext db) : IPlagiarismReportRepository
{
    private readonly FileAnalysisDbContext _db = db;

    public void Add(PlagiarismReport report)
    {
        _db.Reports.Add(report);
    }

    public PlagiarismReport? GetByFileId(Guid fileId)
    {
        return _db.Reports.FirstOrDefault(report => report.FileId == fileId);
    }

    public IReadOnlyCollection<PlagiarismReport> GetByWorkId(Guid workId)
    {
        return [.. _db.Reports.Where(report => report.WorkId == workId)];
    }

    public void SaveChanges()
    {
        _db.SaveChanges();
    }
}
