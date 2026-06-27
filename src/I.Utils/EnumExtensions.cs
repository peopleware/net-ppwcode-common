using PPWCode.Vernacular.Exceptions.V;

namespace PPWCode.Common.I.Utils;

/// <summary>
///     Provides extension methods for working with enumeration values.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    ///     Returns the individual flag values that are set on the specified enumeration value.
    /// </summary>
    /// <typeparam name="TEnum">The type of the enumeration.</typeparam>
    /// <param name="value">The enumeration value whose individual flags are returned.</param>
    /// <returns>
    ///     A sequence containing the power-of-two flag values defined on <typeparamref name="TEnum" /> that are
    ///     set on <paramref name="value" />.
    /// </returns>
    public static IEnumerable<TEnum> GetIndividualFlags<TEnum>(this TEnum value)
        where TEnum : Enum
        => Enum
            .GetValues(typeof(TEnum))
            .OfType<TEnum>()
            .Where(e =>
            {
                long i = Convert.ToInt64(e);
                return ((i & (i - 1)) == 0) && value.HasFlag(e);
            });

    /// <summary>
    ///     Parses the specified string into an enumeration value of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">The enumeration type to parse.</typeparam>
    /// <param name="value">The text representation of the enumeration value.</param>
    /// <param name="ignoreCase"><see langword="true" /> to ignore case during parsing; otherwise, <see langword="false" />.</param>
    /// <returns>The parsed enumeration value.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="value" /> is <see langword="null" />.</exception>
    /// <exception cref="ArgumentException">
    ///     <paramref name="value" /> does not map to a valid value of
    ///     <typeparamref name="T" />.
    /// </exception>
    public static T Parse<T>(this string value, bool ignoreCase = false)
        where T : struct
        => (T)Enum.Parse(typeof(T), value, ignoreCase);

    /// <summary>
    ///     Converts the specified integral value to an enumeration value of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">The enumeration type to convert to.</typeparam>
    /// <param name="value">The integral value to convert.</param>
    /// <returns>The enumeration value represented by <paramref name="value" />.</returns>
    /// <exception cref="ProgrammingError"><typeparamref name="T" /> does not define <paramref name="value" />.</exception>
    public static T Parse<T>(this int value)
        where T : struct
    {
        if (Enum.IsDefined(typeof(T), value))
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        throw new ProgrammingError($"{typeof(T).FullName} is not a defined enumeration type.");
    }

    /// <summary>
    ///     Attempts to parse the specified string into an enumeration value of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">The enumeration type to parse.</typeparam>
    /// <param name="value">The text representation of the enumeration value.</param>
    /// <param name="ignoreCase"><see langword="true" /> to ignore case during parsing; otherwise, <see langword="false" />.</param>
    /// <param name="fallbackValue">
    ///     The value to return when parsing fails or when <paramref name="value" /> is
    ///     <see langword="null" />.
    /// </param>
    /// <returns>The parsed enumeration value, or <paramref name="fallbackValue" /> when parsing does not succeed.</returns>
    /// <exception cref="ProgrammingError"><typeparamref name="T" /> is not an enumeration type.</exception>
    public static T? TryParse<T>(this string? value, bool ignoreCase, T? fallbackValue = null)
        where T : struct
    {
        if (typeof(T).IsEnum)
        {
            return
                value != null
                    ? Enum.TryParse(value, ignoreCase, out T @enum) ? @enum : fallbackValue
                    : fallbackValue;
        }

        throw new ProgrammingError($"{typeof(T).FullName} is not a defined enumeration type.");
    }

    /// <summary>
    ///     Attempts to parse the specified string into an enumeration value of type <typeparamref name="T" />,
    ///     ignoring character casing.
    /// </summary>
    /// <typeparam name="T">The enumeration type to parse.</typeparam>
    /// <param name="value">The text representation of the enumeration value.</param>
    /// <param name="fallbackValue">
    ///     The value to return when parsing fails or when <paramref name="value" /> is
    ///     <see langword="null" />.
    /// </param>
    /// <returns>The parsed enumeration value, or <paramref name="fallbackValue" /> when parsing does not succeed.</returns>
    public static T? TryParse<T>(this string? value, T? fallbackValue)
        where T : struct
        => value.TryParse(true, fallbackValue);

    /// <summary>
    ///     Attempts to convert the specified integral value to an enumeration value of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">The enumeration type to convert to.</typeparam>
    /// <param name="value">The integral value to convert.</param>
    /// <param name="fallbackValue">
    ///     The value to return when <paramref name="value" /> is not defined on
    ///     <typeparamref name="T" />.
    /// </param>
    /// <returns>
    ///     The corresponding enumeration value, or <paramref name="fallbackValue" /> when <paramref name="value" /> is
    ///     not defined.
    /// </returns>
    public static T? TryParse<T>(this int value, T? fallbackValue)
        where T : struct
        => Enum.IsDefined(typeof(T), value) ? (T?)Enum.ToObject(typeof(T), value) : fallbackValue;

    /// <summary>
    ///     Returns all defined values of the enumeration type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">The enumeration type whose values are returned.</typeparam>
    /// <returns>A sequence containing every value defined on <typeparamref name="T" />.</returns>
    public static IEnumerable<T> GetValues<T>()
        where T : struct
        => Enum.GetValues(typeof(T)).Cast<T>();
}
