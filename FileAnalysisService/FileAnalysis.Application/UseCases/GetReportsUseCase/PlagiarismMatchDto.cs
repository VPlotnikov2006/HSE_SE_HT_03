namespace FileAnalysis.Application.UseCases.GetReportsUseCase;

public record class PlagiarismMatchDto(Guid SourceFileId, string SourceOwner, double Similarity)
{

}
