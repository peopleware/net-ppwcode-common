using NUnit.Framework;

namespace PPWCode.Common.I.Utils.Tests;

[TestFixture]
[Parallelizable(ParallelScope.Fixtures)]
public abstract class BaseTests
{
    [SetUp]
    public void Setup()
    {
        OnSetup();
    }

    [TearDown]
    public void TearDown()
    {
        OnTearDown();
    }

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        OnOneTimeSetup();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        OnOneTimeTearDown();
    }

    private static int _nextId = 1000000;

    protected static int NextId
        => Interlocked.Increment(ref _nextId);

    protected virtual void OnOneTimeTearDown()
    {
    }

    protected virtual void OnOneTimeSetup()
    {
    }

    protected virtual void OnSetup()
    {
    }

    protected virtual void OnTearDown()
    {
    }
}
