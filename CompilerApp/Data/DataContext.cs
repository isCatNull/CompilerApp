using CompilerApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompilerApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompilationSummary>().ToTable("CompilationSummary");
        }

        public DbSet<CompilationSummary> Summaries { get; set; }
    }
}
