using NUnit.Framework;

namespace PPWCode.Common.I.Utils.Tests.Extensions;

public class DateTimeExtensionsTest : BaseTests
{
    [Test]
    [TestCaseSource(typeof(DateTimeExtensionsFactory), nameof(DateTimeExtensionsFactory.AddQuarters))]
    public DateTime AddQuartersTest(DateTime dateTime, int quarters)
        => dateTime.AddQuarters(quarters);

    [Test]
    [TestCaseSource(typeof(DateTimeExtensionsFactory), nameof(DateTimeExtensionsFactory.AddMonthsForNullableDatetime))]
    public DateTime? AddMonthsForNullableDatetimeTest(DateTime? dateTime, int numberOfMonths)
        => dateTime.AddMonths(numberOfMonths);

    [Test]
    [TestCaseSource(typeof(DateTimeExtensionsFactory), nameof(DateTimeExtensionsFactory.IsDate))]
    public bool IsDateTest(DateTime dateTime)
        => dateTime.IsDate();

    [Test]
    [TestCaseSource(typeof(DateTimeExtensionsFactory), nameof(DateTimeExtensionsFactory.IsFirstDayOfMonthForNullableDatetime))]
    public bool IsDateNullableDatetimeTest(DateTime? dateTime)
        => dateTime.IsDate();

    [Test]
    [TestCaseSource(typeof(DateTimeExtensionsFactory), nameof(DateTimeExtensionsFactory.IsFirstDayOfQuarter))]
    public bool IsFirstDayOfQuarterTest(DateTime dateTime)
        => dateTime.IsFirstDayOfQuarter();

    [Test]
    [Description("DateTimeExtensions FirstDayOfCurrentQuarter")]
    public void TestFirstDayOfCurrentQuarter()
    {
        Assert.That(new DateTime(2000, 1, 1), Is.EqualTo(new DateTime(2000, 3, 28).FirstDayOfQuarter()));
        Assert.That(new DateTime(2000, 10, 1), Is.EqualTo(new DateTime(2000, 12, 31).FirstDayOfQuarter()));
        Assert.That(new DateTime(2000, 7, 1), Is.EqualTo(new DateTime(2000, 7, 1).FirstDayOfQuarter()));
    }

    [Test]
    [Description("DateTimeExtensions FirstDayOfNextQuarter")]
    public void TestFirstDayOfNextQuarter()
    {
        Assert.That(new DateTime(2000, 4, 1), Is.EqualTo(new DateTime(2000, 3, 28).FirstDayOfNextQuarter()));
        Assert.That(new DateTime(2001, 1, 1), Is.EqualTo(new DateTime(2000, 12, 31).FirstDayOfNextQuarter()));
        Assert.That(new DateTime(2000, 10, 1), Is.EqualTo(new DateTime(2000, 7, 1).FirstDayOfNextQuarter()));
        Assert.That(new DateTime(2011, 1, 1), Is.EqualTo(new DateTime(2010, 10, 1).FirstDayOfNextQuarter()));
    }

    [Test]
    [TestCaseSource(typeof(DateTimeExtensionsFactory), nameof(DateTimeExtensionsFactory.FirstDayOfNextMonth))]
    public DateTime FirstDayOfNextMonthTest(DateTime dateTime)
        => dateTime.FirstDayOfNextMonth();

    [Test]
    [TestCaseSource(typeof(DateTimeExtensionsFactory), nameof(DateTimeExtensionsFactory.IsFirstDayOfMonth))]
    public bool IsFirstDayOfMonthTest(DateTime dateTime)
        => dateTime.IsFirstDayOfMonth();

    [Test]
    [TestCaseSource(typeof(DateTimeExtensionsFactory), nameof(DateTimeExtensionsFactory.IsFirstDayOfMonthForNullableDatetime))]
    public bool IsFirstDayOfMonthForNullableDatetimeTest(DateTime? dateTime)
        => dateTime.IsFirstDayOfMonth();

    [Test]
    [TestCaseSource(typeof(DateTimeExtensionsFactory), nameof(DateTimeExtensionsFactory.GetUtcDateTime))]
    public DateTime GetUtcDateTimeTest(DateTime dateTime)
    {
        // Arrange
        DateTime expected = new(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, DateTimeKind.Utc);
        DateTime actual = DateTimeExtensions.GetUtcDateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);

        // Assert
        Assert.That(expected, Is.EqualTo(actual));
        Assert.That(expected.Kind, Is.EqualTo(actual.Kind));
        return actual;
    }

    [Test]
    [TestCaseSource(typeof(DateTimeExtensionsFactory), nameof(DateTimeExtensionsFactory.GetUtcDate))]
    public DateTime GetUtcDateTest(DateTime dateTime)
    {
        // Arrange
        DateTime expected = new(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, DateTimeKind.Utc);
        DateTime actual = DateTimeExtensions.GetUtcDate(dateTime.Year, dateTime.Month, dateTime.Day);

        // Assert
        Assert.That(expected, Is.EqualTo(actual));
        Assert.That(expected.Kind, Is.EqualTo(actual.Kind));
        return actual;
    }
}
