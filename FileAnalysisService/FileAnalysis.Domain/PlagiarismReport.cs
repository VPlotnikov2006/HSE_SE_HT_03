namespace FileAnalysis.Domain;

public class PlagiarismReport(Guid fileId, Guid workId, string owner)
{
    public Guid ReportId { get; private set; } = Guid.NewGuid();
    public Guid FileId { get; private set; } = fileId;
    public Guid WorkId { get; private set; } = workId;
    public string Owner { get; private set; } = owner;

    private readonly List<PlagiarismMatch> _matches = [];
    public IReadOnlyCollection<PlagiarismMatch> Matches => _matches;

    public DateTime CheckedAt { get; private set; } = DateTime.UtcNow;

    public void AddMatch(Guid sourceFileId, string sourceOwner, double similarity)
    {
        _matches.Add(new PlagiarismMatch(
            sourceFileId,
            ReportId,
            sourceOwner,
            similarity
        ));
    }
}
