using Microsoft.EntityFrameworkCore;
using TestWebAPI.Model;

public class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options) { }

    public DbSet<Test> Tests { get; set; } = null!;
}
