using NUnit.Framework;

namespace PPWCode.Common.I.Utils.Tests.Extensions;

public class DateTimeOffsetExtensionsTest : BaseTests
{
    [Test]
    [TestCaseSource(typeof(DateTimeOffsetExtensionsFactory), nameof(DateTimeOffsetExtensionsFactory.AddQuarters))]
    public DateTimeOffset AddQuartersTest(DateTimeOffset dateTime, int quarters)
        => dateTime.AddQuarters(quarters);

    [Test]
    [TestCaseSource(typeof(DateTimeOffsetExtensionsFactory), nameof(DateTimeOffsetExtensionsFactory.AddMonthsForNullableDatetime))]
    public DateTimeOffset? AddMonthsForNullableDatetimeTest(DateTimeOffset? dateTime, int numberOfMonths)
        => dateTime.AddMonths(numberOfMonths);

    [Test]
    [TestCaseSource(typeof(DateTimeOffsetExtensionsFactory), nameof(DateTimeOffsetExtensionsFactory.IsDate))]
    public bool IsDateTest(DateTimeOffset dateTime)
        => dateTime.IsDate();

    [Test]
    [TestCaseSource(typeof(DateTimeOffsetExtensionsFactory), nameof(DateTimeOffsetExtensionsFactory.IsFirstDayOfMonthForNullableDatetime))]
    public bool IsDateNullableDatetimeTest(DateTimeOffset? dateTime)
        => dateTime.IsDate();

    [Test]
    [TestCaseSource(typeof(DateTimeOffsetExtensionsFactory), nameof(DateTimeOffsetExtensionsFactory.IsFirstDayOfQuarter))]
    public bool IsFirstDayOfQuarterTest(DateTimeOffset dateTime)
        => dateTime.IsFirstDayOfQuarter();

    [Test]
    [Description("DateTimeExtensions FirstDayOfCurrentQuarter")]
    public void TestFirstDayOfCurrentQuarter()
    {
        Assert.That(new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero), Is.EqualTo(new DateTimeOffset(2000, 3, 28, 0, 0, 0, TimeSpan.Zero).FirstDayOfQuarter()));
        Assert.That(new DateTimeOffset(2000, 10, 1, 0, 0, 0, TimeSpan.Zero), Is.EqualTo(new DateTimeOffset(2000, 12, 31, 0, 0, 0, TimeSpan.Zero).FirstDayOfQuarter()));
        Assert.That(new DateTimeOffset(2000, 7, 1, 0, 0, 0, TimeSpan.Zero), Is.EqualTo(new DateTimeOffset(2000, 7, 1, 0, 0, 0, TimeSpan.Zero).FirstDayOfQuarter()));
    }

    [Test]
    [Description("DateTimeExtensions FirstDayOfNextQuarter")]
    public void TestFirstDayOfNextQuarter()
    {
        Assert.That(new DateTimeOffset(2000, 4, 1, 0, 0, 0, TimeSpan.Zero), Is.EqualTo(new DateTimeOffset(2000, 3, 28, 0, 0, 0, TimeSpan.Zero).FirstDayOfNextQuarter()));
        Assert.That(new DateTimeOffset(2001, 1, 1, 0, 0, 0, TimeSpan.Zero), Is.EqualTo(new DateTimeOffset(2000, 12, 31, 0, 0, 0, TimeSpan.Zero).FirstDayOfNextQuarter()));
        Assert.That(new DateTimeOffset(2000, 10, 1, 0, 0, 0, TimeSpan.Zero), Is.EqualTo(new DateTimeOffset(2000, 7, 1, 0, 0, 0, TimeSpan.Zero).FirstDayOfNextQuarter()));
        Assert.That(new DateTimeOffset(2011, 1, 1, 0, 0, 0, TimeSpan.Zero), Is.EqualTo(new DateTimeOffset(2010, 10, 1, 0, 0, 0, TimeSpan.Zero).FirstDayOfNextQuarter()));
    }

    [Test]
    [TestCaseSource(typeof(DateTimeOffsetExtensionsFactory), nameof(DateTimeOffsetExtensionsFactory.FirstDayOfNextMonth))]
    public DateTimeOffset FirstDayOfNextMonthTest(DateTimeOffset dateTime)
        => dateTime.FirstDayOfNextMonth();

    [Test]
    [TestCaseSource(typeof(DateTimeOffsetExtensionsFactory), nameof(DateTimeOffsetExtensionsFactory.IsFirstDayOfMonth))]
    public bool IsFirstDayOfMonthTest(DateTimeOffset dateTime)
        => dateTime.IsFirstDayOfMonth();

    [Test]
    [TestCaseSource(typeof(DateTimeOffsetExtensionsFactory), nameof(DateTimeOffsetExtensionsFactory.IsFirstDayOfMonthForNullableDatetime))]
    public bool IsFirstDayOfMonthForNullableDatetimeTest(DateTimeOffset? dateTime)
        => dateTime.IsFirstDayOfMonth();

    [Test]
    [TestCaseSource(typeof(DateTimeOffsetExtensionsFactory), nameof(DateTimeOffsetExtensionsFactory.GetUtcDateTime))]
    public DateTimeOffset GetUtcDateTimeTest(DateTimeOffset dateTime)
    {
        // Arrange
        DateTimeOffset expected = new(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, TimeSpan.Zero);
        DateTimeOffset actual = DateTimeExtensions.GetUtcDateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);

        // Assert
        Assert.That(expected, Is.EqualTo(actual));
        Assert.That(expected.Offset, Is.EqualTo(actual.Offset));
        return actual;
    }

    [Test]
    [TestCaseSource(typeof(DateTimeOffsetExtensionsFactory), nameof(DateTimeOffsetExtensionsFactory.GetUtcDate))]
    public DateTimeOffset GetUtcDateTest(DateTimeOffset dateTime)
    {
        // Arrange
        DateTimeOffset expected = new(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, TimeSpan.Zero);
        DateTimeOffset actual = DateTimeExtensions.GetUtcDate(dateTime.Year, dateTime.Month, dateTime.Day);

        // Assert
        Assert.That(expected, Is.EqualTo(actual));
        Assert.That(expected.Offset, Is.EqualTo(actual.Offset));
        return actual;
    }
}
