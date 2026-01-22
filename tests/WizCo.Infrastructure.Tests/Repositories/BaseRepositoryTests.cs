using WizCo.Infrastructure.Tests.Fixtures;

namespace WizCo.Infrastructure.Tests.Repositories;

public abstract class BaseRepositoryTests : IClassFixture<WizCoDbContextFixture>
{
    protected readonly WizCoDbContextFixture DbContextFixture;

    protected BaseRepositoryTests(WizCoDbContextFixture dbContextFixture)
    {
        DbContextFixture = dbContextFixture;
        DbContextFixture.ResetDatabase();
    }
}
