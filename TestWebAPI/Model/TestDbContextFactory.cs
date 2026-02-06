using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TestWebAPI.Model
{
    public class TestDbContextFactory : IDesignTimeDbContextFactory<TestDbContext>
    {
        public TestDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TestDbContext>();

            // Railway PostgreSQL connection string (used only for migrations)
            optionsBuilder.UseNpgsql(
                "Host=gondola.proxy.rlwy.net;Port=41249;Database=railway;Username=postgres;Password=pMaIYPlnkzfmJpHtlmxTrBlaKkOVEEJc;SslMode=Require;Trust Server Certificate=true"
            );

            return new TestDbContext(optionsBuilder.Options);
        }
    }
}
