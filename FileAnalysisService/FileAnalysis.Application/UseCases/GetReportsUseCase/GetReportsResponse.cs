using FileAnalysis.Domain.Entities;

namespace FileAnalysis.Application.UseCases.GetReportsUseCase;

public record class GetReportsResponse(
    Guid ReportId,
    Guid FileId,
    string Owner,
    double HighestSimilarity,
    IReadOnlyCollection<PlagiarismMatchDto> Matches
)
{

}
