namespace Gateway.Application.DTOs.FileAnalysisDTOs.GetReports;

public record class GetReportsResponse(
    Guid ReportId,
    Guid FileId,
    string Owner,
    double HighestSimilarity,
    IReadOnlyCollection<PlagiarismMatchDto> Matches
)
{
}
