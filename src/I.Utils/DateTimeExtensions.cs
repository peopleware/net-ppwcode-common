namespace PPWCode.Common.I.Utils;

/// <summary>
///     Provides helper and extension methods for working with <see cref="DateTime" /> values.
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    ///     Determines whether the specified nullable value represents a date without a time component.
    /// </summary>
    /// <param name="dt">The value to inspect.</param>
    /// <returns>
    ///     <see langword="true" /> when <paramref name="dt" /> has a value and its time of day is midnight; otherwise,
    ///     <see langword="false" />.
    /// </returns>
    public static bool IsDate(this DateTime? dt)
        => dt.HasValue && dt.Value.IsDate();

    /// <summary>
    ///     Determines whether the specified value represents a date without a time component.
    /// </summary>
    /// <param name="dt">The value to inspect.</param>
    /// <returns><see langword="true" /> when the time of day is midnight; otherwise, <see langword="false" />.</returns>
    public static bool IsDate(this DateTime dt)
        => dt.TimeOfDay == TimeSpan.Zero;

    /// <summary>
    ///     Adds the specified number of quarters to a value.
    ///     If the resulting day is not valid in the target month, the last valid day of that month is used.
    /// </summary>
    /// <param name="dt">The value to adjust.</param>
    /// <param name="quarters">The number of quarters to add. Negative values subtract quarters.</param>
    /// <returns>A new value with the requested quarter offset applied.</returns>
    public static DateTime AddQuarters(this DateTime dt, int quarters)
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
    public static DateTime? AddMonths(this DateTime? dt, int months)
        => dt?.AddMonths(months);

    /// <summary>
    ///     Creates a UTC <see cref="DateTime" /> from the supplied date and time parts.
    /// </summary>
    /// <param name="year">The year component.</param>
    /// <param name="month">The month component.</param>
    /// <param name="day">The day component.</param>
    /// <param name="hours">The hour component.</param>
    /// <param name="minutes">The minute component.</param>
    /// <param name="seconds">The second component.</param>
    /// <returns>A <see cref="DateTime" /> whose kind is <see cref="DateTimeKind.Utc" />.</returns>
    public static DateTime GetUtcDateTime(
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
            DateTimeKind.Utc);

    /// <summary>
    ///     Creates a UTC date value from the supplied date parts.
    /// </summary>
    /// <param name="year">The year component.</param>
    /// <param name="month">The month component.</param>
    /// <param name="day">The day component.</param>
    /// <returns>A midnight <see cref="DateTime" /> whose kind is <see cref="DateTimeKind.Utc" />.</returns>
    public static DateTime GetUtcDate(int year, int month, int day)
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
    /// <returns>A value representing the first day of the quarter at midnight, preserving the original kind.</returns>
    public static DateTime FirstDayOfQuarter(this DateTime dt)
    {
        int months = (((dt.Month - 1) / 3) * 3) + 1;
        DateTime result = new(
            dt.Year,
            months,
            1,
            0,
            0,
            0,
            dt.Kind);
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
    public static bool IsFirstDayOfQuarter(this DateTime dt)
        => dt.IsDate() && (dt.Day == 1) && ((dt.Month - 1) % 3 == 0);

    /// <summary>
    ///     Returns the first day of the quarter that contains the specified nullable value.
    /// </summary>
    /// <param name="dt">The value whose quarter boundary is requested.</param>
    /// <returns>
    ///     The first day of the quarter at midnight, preserving the original kind, or <see langword="null" /> when
    ///     <paramref name="dt" /> is <see langword="null" />.
    /// </returns>
    public static DateTime? FirstDayOfQuarter(this DateTime? dt)
        => dt?.FirstDayOfQuarter();

    /// <summary>
    ///     Returns the first day of the quarter immediately following the specified value.
    /// </summary>
    /// <param name="dt">The value whose next quarter boundary is requested.</param>
    /// <returns>A value representing the first day of the next quarter at midnight.</returns>
    public static DateTime FirstDayOfNextQuarter(this DateTime dt)
        => dt.FirstDayOfQuarter().AddQuarters(1);

    /// <summary>
    ///     Returns the first day of the quarter immediately following the specified nullable value.
    /// </summary>
    /// <param name="dt">The value whose next quarter boundary is requested.</param>
    /// <returns>
    ///     The first day of the next quarter at midnight, or <see langword="null" /> when <paramref name="dt" /> is
    ///     <see langword="null" />.
    /// </returns>
    public static DateTime? FirstDayOfNextQuarter(this DateTime? dt)
        => dt?.FirstDayOfNextQuarter();

    /// <summary>
    ///     Determines whether the specified value is the first day of a month.
    /// </summary>
    /// <param name="dt">The value to inspect.</param>
    /// <returns>
    ///     <see langword="true" /> when <paramref name="dt" /> is the first calendar day of a month and has no time
    ///     component; otherwise, <see langword="false" />.
    /// </returns>
    public static bool IsFirstDayOfMonth(this DateTime dt)
        => dt.IsDate() && (dt.Day == 1);

    /// <summary>
    ///     Determines whether the specified nullable value is the first day of a month.
    /// </summary>
    /// <param name="dt">The value to inspect.</param>
    /// <returns>
    ///     <see langword="true" /> when <paramref name="dt" /> has a value that is the first calendar day of a month and
    ///     has no time component; otherwise, <see langword="false" />.
    /// </returns>
    public static bool IsFirstDayOfMonth(this DateTime? dt)
        => dt.HasValue && dt.Value.IsDate() && (dt.Value.Day == 1);

    /// <summary>
    ///     Returns the first day of the month immediately following the specified value.
    /// </summary>
    /// <param name="dt">The value whose next month boundary is requested.</param>
    /// <returns>A value representing the first day of the next month at midnight, preserving the original kind.</returns>
    public static DateTime FirstDayOfNextMonth(this DateTime dt)
    {
        DateTime ndt = dt.AddMonths(1);
        return new DateTime(
            ndt.Year,
            ndt.Month,
            1,
            0,
            0,
            0,
            dt.Kind);
    }

    /// <summary>
    ///     Returns the first day of the month immediately following the specified nullable value.
    /// </summary>
    /// <param name="dt">The value whose next month boundary is requested.</param>
    /// <returns>
    ///     The first day of the next month at midnight, or <see langword="null" /> when <paramref name="dt" /> is
    ///     <see langword="null" />.
    /// </returns>
    public static DateTime? FirstDayOfNextMonth(this DateTime? dt)
        => dt?.FirstDayOfNextMonth();

    /// <summary>
    ///     Returns the first day of the month that contains the specified value.
    /// </summary>
    /// <param name="dt">The value whose month boundary is requested.</param>
    /// <returns>A value representing the first day of the month at midnight, preserving the original kind.</returns>
    public static DateTime FirstDayOfMonth(this DateTime dt)
        => new(
            dt.Year,
            dt.Month,
            1,
            0,
            0,
            0,
            dt.Kind);

    /// <summary>
    ///     Returns the first day of the month that contains the specified nullable value.
    /// </summary>
    /// <param name="dt">The value whose month boundary is requested.</param>
    /// <returns>
    ///     The first day of the month at midnight, or <see langword="null" /> when <paramref name="dt" /> is
    ///     <see langword="null" />.
    /// </returns>
    public static DateTime? FirstDayOfMonth(this DateTime? dt)
        => dt?.FirstDayOfMonth();

    /// <summary>
    ///     Returns the specified value, or <see cref="DateTime.MinValue" /> when the value is <see langword="null" />.
    /// </summary>
    /// <param name="datetime">The nullable value to coalesce.</param>
    /// <returns><paramref name="datetime" /> when it has a value; otherwise, <see cref="DateTime.MinValue" />.</returns>
    public static DateTime CoalesceStartValue(this DateTime? datetime)
        => datetime ?? DateTime.MinValue;

    /// <summary>
    ///     Returns the specified value, or <see cref="DateTime.MaxValue" /> when the value is <see langword="null" />.
    /// </summary>
    /// <param name="datetime">The nullable value to coalesce.</param>
    /// <returns><paramref name="datetime" /> when it has a value; otherwise, <see cref="DateTime.MaxValue" />.</returns>
    public static DateTime CoalesceEndValue(this DateTime? datetime)
        => datetime ?? DateTime.MaxValue;
}
