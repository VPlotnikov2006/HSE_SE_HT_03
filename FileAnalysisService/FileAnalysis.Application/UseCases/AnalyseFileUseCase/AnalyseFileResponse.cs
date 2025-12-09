namespace FileAnalysis.Application.UseCases.AnalyseFileUseCase;

public record class AnalyseFileResponse(
    Guid ReportId,
    double HighestSimilarity
)
{
}
