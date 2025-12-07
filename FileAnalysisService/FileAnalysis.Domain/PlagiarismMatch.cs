namespace FileAnalysis.Domain;

public class PlagiarismMatch(Guid sourceFileId, Guid reportId, string sourceOwner, double similarity)
{
    public Guid MatchId { get; private set; } = Guid.NewGuid();
    public Guid ReportId { get; private set; } = reportId;

    public Guid SourceFileId { get; private set; } = sourceFileId;
    public string SourceOwner { get; private set; } = sourceOwner;
    public double Similarity { get; private set; } = similarity;
}
