namespace Gateway.Application.DTOs.FileAnalysisDTOs.GetReports;

public record class PlagiarismMatchDto(Guid SourceFileId, string SourceOwner, double Similarity)
{
}
