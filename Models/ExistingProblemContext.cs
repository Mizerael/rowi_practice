using Microsoft.EntityFrameworkCore;

namespace rowi_practice.Models;

public class ExistingProblemContext : DbContext 
{
    public ExistingProblemContext(DbContextOptions<ExistingProblemContext> opt) : base(opt) {}

    public DbSet<ExistingProblem> ExistingProblems { get; set; } = null!;
    
}
