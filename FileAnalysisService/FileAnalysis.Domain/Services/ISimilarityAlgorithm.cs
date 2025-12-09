namespace FileAnalysis.Domain.Services;

public interface ISimilarityAlgorithm
{
    public double Calculate(byte[] a, byte[] b);

    public bool IsPlagiarism(double score);
}
