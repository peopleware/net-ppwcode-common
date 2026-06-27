using System.Collections;

using NUnit.Framework;

namespace PPWCode.Common.I.Utils.Tests.Extensions;

public class DateTimeExtensionsFactory
{
    public static IEnumerable AddQuarters
    {
        get
        {
            yield return new TestCaseData(D(2015, 05, 01), 1).Returns(D(2015, 08, 01));
            yield return new TestCaseData(D(2015, 08, 01), -1).Returns(D(2015, 05, 01));
            yield return new TestCaseData(D(2015, 03, 31), 1).Returns(D(2015, 06, 30));
            yield return new TestCaseData(D(2015, 02, 28), 1).Returns(D(2015, 05, 28));
            yield return new TestCaseData(D(2014, 11, 30), 1).Returns(D(2015, 02, 28));
            yield return new TestCaseData(D(2014, 11, 29), 1).Returns(D(2015, 02, 28));
            yield return new TestCaseData(D(2014, 11, 28), 1).Returns(D(2015, 02, 28));
        }
    }

    public static IEnumerable AddMonthsForNullableDatetime
    {
        get { yield return new TestCaseData((DateTime?)null, 5).Returns(null); }
    }

    public static IEnumerable IsDate
    {
        get
        {
            yield return new TestCaseData(D(2015, 01, 01)).Returns(true);
            yield return new TestCaseData(D(2015, 04, 01)).Returns(true);
            yield return new TestCaseData(D(2015, 07, 01)).Returns(true);
            yield return new TestCaseData(D(2015, 10, 01)).Returns(true);
            yield return new TestCaseData(D(2015, 02, 01, 1)).Returns(false);
            yield return new TestCaseData(D(2015, 03, 01, 4, 15, 15)).Returns(false);
            yield return new TestCaseData(D(2015, 05, 01, 8, 30, 30)).Returns(false);
            yield return new TestCaseData(D(2015, 06, 01, 12, 45, 45)).Returns(false);
        }
    }

    public static IEnumerable IsFirstDayOfQuarter
    {
        get
        {
            yield return new TestCaseData(D(2015, 01, 01)).Returns(true);
            yield return new TestCaseData(D(2015, 04, 01)).Returns(true);
            yield return new TestCaseData(D(2015, 07, 01)).Returns(true);
            yield return new TestCaseData(D(2015, 10, 01)).Returns(true);
            yield return new TestCaseData(D(2015, 02, 01)).Returns(false);
            yield return new TestCaseData(D(2015, 03, 01)).Returns(false);
            yield return new TestCaseData(D(2015, 05, 01)).Returns(false);
            yield return new TestCaseData(D(2015, 06, 01)).Returns(false);
            yield return new TestCaseData(D(2015, 08, 01)).Returns(false);
            yield return new TestCaseData(D(2015, 09, 01)).Returns(false);
            yield return new TestCaseData(D(2015, 11, 01)).Returns(false);
            yield return new TestCaseData(D(2015, 12, 01)).Returns(false);
            yield return new TestCaseData(D(2015, 01, 02)).Returns(false);
            yield return new TestCaseData(D(2015, 02, 02)).Returns(false);
            yield return new TestCaseData(D(2015, 03, 30)).Returns(false);

            yield return new TestCaseData(D(2015, 01, 01, 1, 10)).Returns(false);
            yield return new TestCaseData(D(2015, 04, 01, 5, 12, 10)).Returns(false);
            yield return new TestCaseData(D(2015, 02, 02, 2, 50, 20)).Returns(false);
            yield return new TestCaseData(D(2015, 03, 30, 10, 40, 30)).Returns(false);
        }
    }

    public static IEnumerable FirstDayOfNextMonth
    {
        get
        {
            yield return new TestCaseData(D(2015, 01, 01)).Returns(D(2015, 02, 01));
            yield return new TestCaseData(D(2015, 02, 01)).Returns(D(2015, 03, 01));
            yield return new TestCaseData(D(2015, 03, 01)).Returns(D(2015, 04, 01));
            yield return new TestCaseData(D(2015, 04, 01)).Returns(D(2015, 05, 01));
            yield return new TestCaseData(D(2015, 05, 01)).Returns(D(2015, 06, 01));
            yield return new TestCaseData(D(2015, 06, 01)).Returns(D(2015, 07, 01));
            yield return new TestCaseData(D(2015, 07, 01)).Returns(D(2015, 08, 01));
            yield return new TestCaseData(D(2015, 08, 01)).Returns(D(2015, 09, 01));
            yield return new TestCaseData(D(2015, 09, 01)).Returns(D(2015, 10, 01));
            yield return new TestCaseData(D(2015, 10, 01)).Returns(D(2015, 11, 01));
            yield return new TestCaseData(D(2015, 11, 01)).Returns(D(2015, 12, 01));
            yield return new TestCaseData(D(2015, 12, 01)).Returns(D(2016, 1, 1));
            yield return new TestCaseData(D(2015, 12, 02)).Returns(D(2016, 1, 1));
            yield return new TestCaseData(D(2015, 02, 28)).Returns(D(2015, 03, 01));
            yield return new TestCaseData(D(2015, 01, 5)).Returns(D(2015, 02, 01));
            yield return new TestCaseData(D(2015, 02, 12)).Returns(D(2015, 03, 01));
            yield return new TestCaseData(D(2015, 03, 20)).Returns(D(2015, 04, 01));
            yield return new TestCaseData(D(2015, 04, 21)).Returns(D(2015, 05, 01));
            yield return new TestCaseData(D(2015, 05, 30)).Returns(D(2015, 06, 01));
            yield return new TestCaseData(D(2015, 06, 4)).Returns(D(2015, 07, 01));
        }
    }

    public static IEnumerable IsFirstDayOfMonth
    {
        get
        {
            yield return new TestCaseData(D(2015, 1, 1)).Returns(true);
            yield return new TestCaseData(D(2015, 2, 1)).Returns(true);
            yield return new TestCaseData(D(2015, 3, 1)).Returns(true);
            yield return new TestCaseData(D(2015, 4, 1)).Returns(true);
            yield return new TestCaseData(D(2015, 5, 1)).Returns(true);
            yield return new TestCaseData(D(2015, 6, 1)).Returns(true);
            yield return new TestCaseData(D(2015, 7, 1)).Returns(true);
            yield return new TestCaseData(D(2015, 8, 1)).Returns(true);
            yield return new TestCaseData(D(2015, 9, 1)).Returns(true);
            yield return new TestCaseData(D(2015, 10, 1)).Returns(true);
            yield return new TestCaseData(D(2015, 11, 1)).Returns(true);
            yield return new TestCaseData(D(2015, 12, 1)).Returns(true);

            yield return new TestCaseData(D(2015, 1, 2)).Returns(false);
            yield return new TestCaseData(D(2015, 2, 3)).Returns(false);
            yield return new TestCaseData(D(2015, 3, 4)).Returns(false);
            yield return new TestCaseData(D(2015, 4, 5)).Returns(false);
            yield return new TestCaseData(D(2015, 5, 6)).Returns(false);
            yield return new TestCaseData(D(2015, 6, 7)).Returns(false);
            yield return new TestCaseData(D(2015, 7, 8)).Returns(false);
            yield return new TestCaseData(D(2015, 8, 9)).Returns(false);
            yield return new TestCaseData(D(2015, 9, 11)).Returns(false);
            yield return new TestCaseData(D(2015, 10, 20)).Returns(false);
            yield return new TestCaseData(D(2015, 11, 25)).Returns(false);
            yield return new TestCaseData(D(2015, 12, 31)).Returns(false);

            yield return new TestCaseData(D(2015, 9, 1, 1)).Returns(false);
            yield return new TestCaseData(D(2015, 10, 1, 8, 10, 5)).Returns(false);
            yield return new TestCaseData(D(2015, 11, 25, 10, 45, 59)).Returns(false);
            yield return new TestCaseData(D(2015, 12, 31, 12, 30, 20)).Returns(false);
        }
    }

    public static IEnumerable IsFirstDayOfMonthForNullableDatetime
    {
        get { yield return new TestCaseData((DateTime?)null).Returns(false); }
    }

    public static IEnumerable GetUtcDateTime
    {
        get
        {
            yield return new TestCaseData(D(2015, 01, 01)).Returns(UtcD(2015, 01, 01));
            yield return new TestCaseData(D(2015, 02, 05)).Returns(UtcD(2015, 02, 05));
            yield return new TestCaseData(D(2015, 03, 07)).Returns(UtcD(2015, 03, 07));
            yield return new TestCaseData(D(2015, 04, 08)).Returns(UtcD(2015, 04, 08));
            yield return new TestCaseData(D(2015, 05, 11)).Returns(UtcD(2015, 05, 11));
            yield return new TestCaseData(D(2015, 06, 21)).Returns(UtcD(2015, 06, 21));
            yield return new TestCaseData(D(2015, 07, 01)).Returns(UtcD(2015, 07, 01));
            yield return new TestCaseData(D(2015, 08, 12)).Returns(UtcD(2015, 08, 12));
            yield return new TestCaseData(D(2015, 09, 13)).Returns(UtcD(2015, 09, 13));
            yield return new TestCaseData(D(2015, 10, 14)).Returns(UtcD(2015, 10, 14));
            yield return new TestCaseData(D(2015, 11, 22)).Returns(UtcD(2015, 11, 22));
            yield return new TestCaseData(D(2016, 12, 29)).Returns(UtcD(2016, 12, 29));
            yield return new TestCaseData(D(2016, 12, 30)).Returns(UtcD(2016, 12, 30));
            yield return new TestCaseData(D(2015, 02, 28)).Returns(UtcD(2015, 02, 28));
            yield return new TestCaseData(D(2015, 01, 5)).Returns(UtcD(2015, 01, 5));
            yield return new TestCaseData(D(2015, 02, 12)).Returns(UtcD(2015, 02, 12));
            yield return new TestCaseData(D(2015, 03, 20)).Returns(UtcD(2015, 03, 20));
            yield return new TestCaseData(D(2015, 04, 21)).Returns(UtcD(2015, 04, 21));
            yield return new TestCaseData(D(2015, 05, 30)).Returns(UtcD(2015, 05, 30));
            yield return new TestCaseData(D(2015, 06, 4)).Returns(UtcD(2015, 06, 4));
        }
    }

    public static IEnumerable GetUtcDate
    {
        get
        {
            yield return new TestCaseData(D(2015, 01, 01)).Returns(UtcD(2015, 01, 01));
            yield return new TestCaseData(D(2015, 02, 05)).Returns(UtcD(2015, 02, 05));
            yield return new TestCaseData(D(2015, 03, 07)).Returns(UtcD(2015, 03, 07));
            yield return new TestCaseData(D(2015, 04, 08)).Returns(UtcD(2015, 04, 08));
            yield return new TestCaseData(D(2015, 05, 11)).Returns(UtcD(2015, 05, 11));
            yield return new TestCaseData(D(2015, 06, 21)).Returns(UtcD(2015, 06, 21));
            yield return new TestCaseData(D(2015, 07, 01)).Returns(UtcD(2015, 07, 01));
            yield return new TestCaseData(D(2015, 08, 12)).Returns(UtcD(2015, 08, 12));
            yield return new TestCaseData(D(2015, 09, 13)).Returns(UtcD(2015, 09, 13));
            yield return new TestCaseData(D(2015, 10, 14)).Returns(UtcD(2015, 10, 14));
            yield return new TestCaseData(D(2015, 11, 22)).Returns(UtcD(2015, 11, 22));
            yield return new TestCaseData(D(2016, 12, 29)).Returns(UtcD(2016, 12, 29));
            yield return new TestCaseData(D(2016, 12, 30)).Returns(UtcD(2016, 12, 30));
            yield return new TestCaseData(D(2015, 02, 28)).Returns(UtcD(2015, 02, 28));
            yield return new TestCaseData(D(2015, 01, 5)).Returns(UtcD(2015, 01, 5));
            yield return new TestCaseData(D(2015, 02, 12)).Returns(UtcD(2015, 02, 12));
            yield return new TestCaseData(D(2015, 03, 20)).Returns(UtcD(2015, 03, 20));
            yield return new TestCaseData(D(2015, 04, 21)).Returns(UtcD(2015, 04, 21));
            yield return new TestCaseData(D(2015, 05, 30)).Returns(UtcD(2015, 05, 30));
            yield return new TestCaseData(D(2015, 06, 4)).Returns(UtcD(2015, 06, 4));
        }
    }

    private static DateTime D(int year, int month, int day, int hour = 0, int minute = 0, int second = 0)
        => new(year, month, day, hour, minute, second);

    private static DateTime UtcD(int year, int month, int day, int hour = 0, int minute = 0, int second = 0)
        => new(year, month, day, hour, minute, second, DateTimeKind.Utc);
}
