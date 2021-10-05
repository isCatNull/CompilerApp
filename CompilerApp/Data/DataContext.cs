using CompilerApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompilerApp.Data
{
    public class DataContext : DbContext
    {
        public DbSet<CompilationSummary>? Summaries { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompilationSummary>().ToTable("CompilationSummary");
        }
    }
}
