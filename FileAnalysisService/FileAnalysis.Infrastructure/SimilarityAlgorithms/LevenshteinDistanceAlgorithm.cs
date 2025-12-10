using FileAnalysis.Domain.Services;

namespace FileAnalysis.Infrastructure.SimilarityAlgorithms;

// Комментарий сделал chatgpt, поскольку я не шарю в этих ваших <list type=>, <c>, <description> и прочее
// source: https://ru.wikipedia.org/wiki/%D0%A0%D0%B0%D1%81%D1%81%D1%82%D0%BE%D1%8F%D0%BD%D0%B8%D0%B5_%D0%9B%D0%B5%D0%B2%D0%B5%D0%BD%D1%88%D1%82%D0%B5%D0%B9%D0%BD%D0%B0
/// <summary>
/// Calculates similarity between two byte sequences using
/// the Levenshtein distance algorithm.
/// </summary>
/// <remarks>
/// <para>
/// The similarity score is calculated using the following formula:
/// </para>
/// <code>
/// score = 1 - (d / maxL) ^ p
/// </code>
/// <para>Where:</para>
/// <list type="bullet">
///   <item><description><c>d</c> — Levenshtein distance</description></item>
///   <item><description><c>maxL</c> — maximum length of the input sequences</description></item>
///   <item>
///     <description>
///       <c>p</c> (see <paramref name="pow"/>) — sensitivity parameter:
///       values greater than 1 reduce the impact of small differences
///       and make the algorithm more tolerant to minor edits.
///     </description>
///   </item>
/// </list>
/// <para>
/// The resulting score is normalized to the range <c>[0; 1]</c>, where:
/// </para>
/// <list type="bullet">
///   <item><description><c>1.0</c> — sequences are identical</description></item>
///   <item><description><c>0.0</c> — sequences are completely different</description></item>
/// </list>
/// </remarks>
/// <param name="pow">
/// Power coefficient used in the similarity formula. 
/// Values greater than 1 soften the penalty for small differences.
/// </param>
public class LevenshteinDistanceAlgorithm(double pow) : ISimilarityAlgorithm
{
    private readonly double _pow = pow;

    public double Calculate(byte[] a, byte[] b)
    {
        int l1 = a.Length;
        int l2 = b.Length;

        if (l1 == 0 && l2 == 0)
        {
            return 1.0;
        }

        int maxL = Math.Max(l1, l2);
        int distance = Distance(a, b);

        return Math.Clamp(1 - Math.Pow(distance * 1.0 / maxL, _pow), 0, 1);
    }

    public bool IsPlagiarism(double score)
    {
        return score > 0.5;
    }

    private static int Distance(byte[] a, byte[] b)
    {
        int l1 = a.Length;
        int l2 = b.Length;

        if (l1 == 0)
        {
            return l2;
        }
        if (l2 == 0)
        {
            return l1;
        }

        if (l1 > l2)
        {
            (a, b, l1, l2) = (b, a, l2, l1);
        }

        int[] curr = new int[l2 + 1];
        int[] prev = new int[l2 + 1];

        for (int j = 0; j <= l2; j++)
        {
            prev[j] = j;
        }

        for (int i = 1; i <= l1; i++)
        {
            curr[0] = i;
            for (int j = 1; j <= l2; j++)
            {
                int cost = (a[i - 1] == b[j - 1]) ? 0 : 1;

                curr[j] = Math.Min(
                    Math.Min(
                        prev[j] + 1,
                        curr[j - 1] + 1
                    ),
                    prev[j - 1] + cost
                );
            }

            (prev, curr) = (curr, prev);
        }

        return prev[l2];
    }
}
