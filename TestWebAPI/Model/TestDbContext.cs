using Microsoft.EntityFrameworkCore;

namespace TestWebAPI.Model
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options)
            : base(options)
        {
        }

        public DbSet<Test> Tests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 🔑 Explicit table mapping (IMPORTANT)
            modelBuilder.Entity<Test>().ToTable("TestTable");
        }
    }
}
