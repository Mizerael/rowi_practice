using Microsoft.EntityFrameworkCore;

using rowi_practice.Models;

namespace rowi_practice.Context;

public class SolutionToProblemContext : DbContext
{
    public
    SolutionToProblemContext(DbContextOptions<SolutionToProblemContext> options) : base (options) {}

    public DbSet<SolutionToProblem> SolutionToProblem { get; set; } = null!;
}