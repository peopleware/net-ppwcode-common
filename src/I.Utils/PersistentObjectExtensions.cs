using PPWCode.Vernacular.Persistence.V;

namespace PPWCode.Common.I.Utils;

/// <summary>
///     Provides extension methods for working with persistent objects.
/// </summary>
public static class PersistentObjectExtensions
{
    /// <summary>
    ///     Returns the parent of <paramref name="child" /> when that parent exists and has the specified
    ///     <paramref name="parentId" />.
    /// </summary>
    /// <typeparam name="TChild">The type of the child object.</typeparam>
    /// <typeparam name="TParent">The type of the parent object.</typeparam>
    /// <typeparam name="TIdentity">The type of the persistent object identity.</typeparam>
    /// <param name="child">The child object whose parent is inspected.</param>
    /// <param name="getParent">A delegate that retrieves the parent from the child.</param>
    /// <param name="parentId">The expected identity of the parent.</param>
    /// <returns>
    ///     The parent object when <paramref name="child" /> is not <see langword="null" />, the parent exists,
    ///     and the parent identity equals <paramref name="parentId" />; otherwise, <see langword="null" />.
    /// </returns>
    public static TParent? GetParentWithId<TChild, TParent, TIdentity>(this TChild? child, Func<TChild, TParent?> getParent, TIdentity parentId)
        where TChild : class, IPersistentObject<TIdentity>
        where TParent : class, IPersistentObject<TIdentity>
        where TIdentity : struct, IEquatable<TIdentity>
    {
        if (child == null)
        {
            return null;
        }

        TParent? parent = getParent(child);
        if (parent == null)
        {
            return null;
        }

        return
            EqualityComparer<TIdentity>.Default.Equals(parent.Id, parentId)
                ? parent
                : null;
    }
}
