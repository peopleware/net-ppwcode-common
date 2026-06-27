namespace PPWCode.Common.I.Utils;

/// <summary>
///     Provides helper and extension methods for working with <see cref="DateTimeOffset" /> values.
/// </summary>
public static class DateTimeOffsetExtensions
{
    /// <summary>
    ///     Determines whether the specified nullable value represents a date without a time component.
    /// </summary>
    /// <param name="dt">The value to inspect.</param>
    /// <returns>
    ///     <see langword="true" /> when <paramref name="dt" /> has a value and its time of day is midnight; otherwise,
    ///     <see langword="false" />.
    /// </returns>
    public static bool IsDate(this DateTimeOffset? dt)
        => dt.HasValue && dt.Value.IsDate();

    /// <summary>
    ///     Determines whether the specified value represents a date without a time component.
    /// </summary>
    /// <param name="dt">The value to inspect.</param>
    /// <returns><see langword="true" /> when the time of day is midnight; otherwise, <see langword="false" />.</returns>
    public static bool IsDate(this DateTimeOffset dt)
        => dt.TimeOfDay == TimeSpan.Zero;

    /// <summary>
    ///     Adds the specified number of quarters to a value.
    ///     If the resulting day is not valid in the target month, the last valid day of that month is used.
    /// </summary>
    /// <param name="dt">The value to adjust.</param>
    /// <param name="quarters">The number of quarters to add. Negative values subtract quarters.</param>
    /// <returns>A new value with the requested quarter offset applied.</returns>
    public static DateTimeOffset AddQuarters(this DateTimeOffset dt, int quarters)
        => dt.AddMonths(3 * quarters);

    /// <summary>
    ///     Adds the specified number of months to a nullable value.
    /// </summary>
    /// <param name="dt">The value to adjust.</param>
    /// <param name="months">The number of months to add. Negative values subtract months.</param>
    /// <returns>
    ///     A new value with the requested month offset applied, or <see langword="null" /> when <paramref name="dt" /> is
    ///     <see langword="null" />.
    /// </returns>
    public static DateTimeOffset? AddMonths(this DateTimeOffset? dt, int months)
        => dt?.AddMonths(months);

    /// <summary>
    ///     Creates a UTC <see cref="DateTimeOffset" /> from the supplied date and time parts.
    /// </summary>
    /// <param name="year">The year component.</param>
    /// <param name="month">The month component.</param>
    /// <param name="day">The day component.</param>
    /// <param name="hours">The hour component.</param>
    /// <param name="minutes">The minute component.</param>
    /// <param name="seconds">The second component.</param>
    /// <returns>A <see cref="DateTimeOffset" /> whose offset is UTC.</returns>
    public static DateTimeOffset GetUtcDateTime(
        int year,
        int month,
        int day,
        int hours,
        int minutes,
        int seconds)
        => new(
            year,
            month,
            day,
            hours,
            minutes,
            seconds,
            TimeSpan.Zero);

    /// <summary>
    ///     Creates a UTC date value from the supplied date parts.
    /// </summary>
    /// <param name="year">The year component.</param>
    /// <param name="month">The month component.</param>
    /// <param name="day">The day component.</param>
    /// <returns>A midnight <see cref="DateTimeOffset" /> whose offset is UTC.</returns>
    public static DateTimeOffset GetUtcDate(int year, int month, int day)
        => GetUtcDateTime(
            year,
            month,
            day,
            0,
            0,
            0);

    /// <summary>
    ///     Returns the first day of the quarter that contains the specified value.
    /// </summary>
    /// <param name="dt">The value whose quarter boundary is requested.</param>
    /// <returns>A value representing the first day of the quarter at midnight, preserving the original offset.</returns>
    public static DateTimeOffset FirstDayOfQuarter(this DateTimeOffset dt)
    {
        int months = (((dt.Month - 1) / 3) * 3) + 1;
        DateTimeOffset result = new(
            dt.Year + (months / 12),
            months % 12,
            1,
            0,
            0,
            0,
            dt.Offset);
        return result;
    }

    /// <summary>
    ///     Determines whether the specified value is the first day of a quarter.
    /// </summary>
    /// <param name="dt">The value to inspect.</param>
    /// <returns>
    ///     <see langword="true" /> when <paramref name="dt" /> is the first day of January, April, July, or October and
    ///     has no time component; otherwise, <see langword="false" />.
    /// </returns>
    public static bool IsFirstDayOfQuarter(this DateTimeOffset dt)
        => dt.IsDate() && (dt.Day == 1) && ((dt.Month - 1) % 3 == 0);

    /// <summary>
    ///     Returns the first day of the quarter that contains the specified nullable value.
    /// </summary>
    /// <param name="dt">The value whose quarter boundary is requested.</param>
    /// <returns>
    ///     The first day of the quarter at midnight, preserving the original offset, or <see langword="null" /> when
    ///     <paramref name="dt" /> is <see langword="null" />.
    /// </returns>
    public static DateTimeOffset? FirstDayOfQuarter(this DateTimeOffset? dt)
        => dt?.FirstDayOfQuarter();

    /// <summary>
    ///     Returns the first day of the quarter immediately following the specified value.
    /// </summary>
    /// <param name="dt">The value whose next quarter boundary is requested.</param>
    /// <returns>A value representing the first day of the next quarter at midnight.</returns>
    public static DateTimeOffset FirstDayOfNextQuarter(this DateTimeOffset dt)
        => dt.FirstDayOfQuarter().AddQuarters(1);

    /// <summary>
    ///     Returns the first day of the quarter immediately following the specified nullable value.
    /// </summary>
    /// <param name="dt">The value whose next quarter boundary is requested.</param>
    /// <returns>
    ///     The first day of the next quarter at midnight, or <see langword="null" /> when <paramref name="dt" /> is
    ///     <see langword="null" />.
    /// </returns>
    public static DateTimeOffset? FirstDayOfNextQuarter(this DateTimeOffset? dt)
        => dt?.FirstDayOfNextQuarter();

    /// <summary>
    ///     Determines whether the specified value is the first day of a month.
    /// </summary>
    /// <param name="dt">The value to inspect.</param>
    /// <returns>
    ///     <see langword="true" /> when <paramref name="dt" /> is the first calendar day of a month and has no time
    ///     component; otherwise, <see langword="false" />.
    /// </returns>
    public static bool IsFirstDayOfMonth(this DateTimeOffset dt)
        => dt.IsDate() && (dt.Day == 1);

    /// <summary>
    ///     Determines whether the specified nullable value is the first day of a month.
    /// </summary>
    /// <param name="dt">The value to inspect.</param>
    /// <returns>
    ///     <see langword="true" /> when <paramref name="dt" /> has a value that is the first calendar day of a month and
    ///     has no time component; otherwise, <see langword="false" />.
    /// </returns>
    public static bool IsFirstDayOfMonth(this DateTimeOffset? dt)
        => dt.HasValue && dt.Value.IsDate() && (dt.Value.Day == 1);

    /// <summary>
    ///     Returns the first day of the month immediately following the specified value.
    /// </summary>
    /// <param name="dt">The value whose next month boundary is requested.</param>
    /// <returns>A value representing the first day of the next month at midnight, preserving the original offset.</returns>
    public static DateTimeOffset FirstDayOfNextMonth(this DateTimeOffset dt)
    {
        DateTimeOffset ndt = dt.AddMonths(1);
        return new DateTimeOffset(
            ndt.Year,
            ndt.Month,
            1,
            0,
            0,
            0,
            dt.Offset);
    }

    /// <summary>
    ///     Returns the first day of the month immediately following the specified nullable value.
    /// </summary>
    /// <param name="dt">The value whose next month boundary is requested.</param>
    /// <returns>
    ///     The first day of the next month at midnight, or <see langword="null" /> when <paramref name="dt" /> is
    ///     <see langword="null" />.
    /// </returns>
    public static DateTimeOffset? FirstDayOfNextMonth(this DateTimeOffset? dt)
        => dt?.FirstDayOfNextMonth();

    /// <summary>
    ///     Returns the first day of the month that contains the specified value.
    /// </summary>
    /// <param name="dt">The value whose month boundary is requested.</param>
    /// <returns>A value representing the first day of the month at midnight, preserving the original offset.</returns>
    public static DateTimeOffset FirstDayOfMonth(this DateTimeOffset dt)
        => new(
            dt.Year,
            dt.Month,
            1,
            0,
            0,
            0,
            dt.Offset);

    /// <summary>
    ///     Returns the first day of the month that contains the specified nullable value.
    /// </summary>
    /// <param name="dt">The value whose month boundary is requested.</param>
    /// <returns>
    ///     The first day of the month at midnight, or <see langword="null" /> when <paramref name="dt" /> is
    ///     <see langword="null" />.
    /// </returns>
    public static DateTimeOffset? FirstDayOfMonth(this DateTimeOffset? dt)
        => dt?.FirstDayOfMonth();

    /// <summary>
    ///     Returns the specified value, or <see cref="DateTimeOffset.MinValue" /> when the value is <see langword="null" />.
    /// </summary>
    /// <param name="datetime">The nullable value to coalesce.</param>
    /// <returns><paramref name="datetime" /> when it has a value; otherwise, <see cref="DateTimeOffset.MinValue" />.</returns>
    public static DateTimeOffset CoalesceStartValue(this DateTimeOffset? datetime)
        => datetime ?? DateTimeOffset.MinValue;

    /// <summary>
    ///     Returns the specified value, or <see cref="DateTimeOffset.MaxValue" /> when the value is <see langword="null" />.
    /// </summary>
    /// <param name="datetime">The nullable value to coalesce.</param>
    /// <returns><paramref name="datetime" /> when it has a value; otherwise, <see cref="DateTimeOffset.MaxValue" />.</returns>
    public static DateTimeOffset CoalesceEndValue(this DateTimeOffset? datetime)
        => datetime ?? DateTimeOffset.MaxValue;
}
