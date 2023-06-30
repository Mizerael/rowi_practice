using Microsoft.EntityFrameworkCore;

using rowi_practice.Models;

namespace rowi_practice.Context;

public class ExistingProblemContext : DbContext 
{
    public
    ExistingProblemContext(DbContextOptions<ExistingProblemContext> options) : base(options) {}

    public DbSet<ExistingProblem> ExistingProblems { get; set; } = null!;
}