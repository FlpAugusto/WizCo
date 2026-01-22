using Microsoft.EntityFrameworkCore;
using WizCo.Infrastructure.Data;

namespace WizCo.Infrastructure.Tests.Fixtures;

public class WizCoDbContextFixture : IDisposable
{
    public WizCoDbContext Context { get; private set; }

    public WizCoDbContextFixture()
    {
        Context = CreateContext();
    }

    public WizCoDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<WizCoDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging()
            .Options;

        var context = new WizCoDbContext(options);
        context.Database.EnsureCreated();

        return context;
    }

    public void ResetDatabase()
    {
        Context.ChangeTracker.Clear();
        Context.Database.EnsureDeleted();
        Context.Database.EnsureCreated();
    }

    public void Dispose()
    {
        Context?.Database.EnsureDeleted();
        Context?.Dispose();
    }
}
