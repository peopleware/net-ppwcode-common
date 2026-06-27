using PPWCode.Vernacular.Exceptions.V;

namespace PPWCode.Common.I.Utils;

/// <summary>
///     Provides helper methods for working with <see cref="SemanticException" /> and
///     <see cref="CompoundSemanticException" /> instances.
/// </summary>
public static class SemanticExceptionExtensions
{
    /// <summary>
    ///     Returns all semantic exceptions contained in the specified compound semantic exception.
    /// </summary>
    /// <param name="cse">The compound semantic exception to flatten.</param>
    /// <returns>
    ///     A flattened sequence of contained <see cref="SemanticException" /> instances. When
    ///     <paramref name="cse" /> is <see langword="null" />, an empty sequence is returned.
    /// </returns>
    public static IEnumerable<SemanticException> GetSemanticExceptions(this CompoundSemanticException? cse)
    {
        if (cse != null)
        {
            foreach (SemanticException se in cse.Elements)
            {
                if (se is CompoundSemanticException cse2)
                {
                    foreach (SemanticException se2 in cse2.GetSemanticExceptions())
                    {
                        yield return se2;
                    }
                }
                else
                {
                    yield return se;
                }
            }
        }
    }

    /// <summary>
    ///     Creates a compacted compound semantic exception that contains the distinct semantic exceptions from the
    ///     specified collection.
    /// </summary>
    /// <param name="compoundSemanticExceptions">The compound semantic exceptions to compact.</param>
    /// <returns>
    ///     A <see cref="CompoundSemanticException" /> containing the distinct semantic exceptions from the supplied
    ///     collection, or <see langword="null" /> when <paramref name="compoundSemanticExceptions" /> is
    ///     <see langword="null" /> or empty.
    /// </returns>
    public static CompoundSemanticException? Compact(this IEnumerable<CompoundSemanticException>? compoundSemanticExceptions)
    {
        if ((compoundSemanticExceptions != null) && compoundSemanticExceptions.Any())
        {
            CompoundSemanticException compactedCompoundSemanticException = new();
            foreach (CompoundSemanticException cse in compoundSemanticExceptions)
            {
                foreach (SemanticException se in cse.GetSemanticExceptions().Where(se => !compactedCompoundSemanticException.ContainsElement(se)))
                {
                    compactedCompoundSemanticException.AddElement(se);
                }
            }

            return compactedCompoundSemanticException;
        }

        return null;
    }
}
