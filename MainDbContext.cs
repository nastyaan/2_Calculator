using Microsoft.EntityFrameworkCore;

namespace _2_Calculator
{
    public class MainDbContext : DbContext 
    {
        public MainDbContext(DbContextOptions<MainDbContext> opt) : base(opt) { }

        public DbSet<CalcModel> CalcModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
