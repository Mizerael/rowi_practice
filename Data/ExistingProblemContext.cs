using rowi_practice.Models;
using Microsoft.EntityFrameworkCore;

namespace rowi_practice.Data;

public class ExistingProblemContext : DbContext 
{
    public ExistingProblemContext(DbContextOptions<ExistingProblemContext> opt) : base(opt) {
        Database.EnsureCreated();
    }

    public DbSet<ExistingProblem> Tasks { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExistingProblem>().ToTable("Task");
    }
}
