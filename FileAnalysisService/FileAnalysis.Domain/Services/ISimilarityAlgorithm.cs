namespace FileAnalysis.Domain.Services;

public interface ISimilarityAlgorithm
{
    public double Calculate(byte[] a, byte[] b);
}
