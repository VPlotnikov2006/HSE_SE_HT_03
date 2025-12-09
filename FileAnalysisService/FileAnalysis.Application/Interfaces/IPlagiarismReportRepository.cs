using FileAnalysis.Domain.Entities;

namespace FileAnalysis.Application.Interfaces;

public interface IPlagiarismReportRepository
{
    void Add(PlagiarismReport report);
    PlagiarismReport? GetByFileId(Guid fileId);
    IReadOnlyCollection<PlagiarismReport> GetByWorkId(Guid workId);

    void SaveChanges();
}
