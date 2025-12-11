namespace Gateway.Application.DTOs.FileAnalysisDTOs.AnalyseFile;

public record class AnalyseFileResponse(
    Guid ReportId,
    double HighestSimilarity
)
{
}
