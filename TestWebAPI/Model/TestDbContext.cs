using Microsoft.EntityFrameworkCore;

namespace TestWebAPI.Model
{
    public class TestDbContext:DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) :base(options) 
        { 
            
        }
        public DbSet<Test> tests { get; set; } // We add the Db set class of our model class Test
    }
}
