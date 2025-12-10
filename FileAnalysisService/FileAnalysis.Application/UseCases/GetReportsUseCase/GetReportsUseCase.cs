using FileAnalysis.Application.Interfaces;

namespace FileAnalysis.Application.UseCases.GetReportsUseCase;

public class GetReportsUseCase(IPlagiarismReportRepository repo)
{
    private readonly IPlagiarismReportRepository _repo = repo;

    public IReadOnlyCollection<GetReportsResponse> Execute(
        GetReportsRequest request)
    {
        var reports = _repo.GetByWorkId(request.WorkId);

        return [.. reports.Select(r => new GetReportsResponse(
            r.ReportId,
            r.FileId,
            r.Owner,
            r.HighestSimilarity,
            [.. r.Matches.Select(m => new PlagiarismMatchDto(m.SourceFileId, m.SourceOwner, m.Similarity))]
        ))];
    }
}
