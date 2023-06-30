using Microsoft.EntityFrameworkCore;

using rowi_practice.Models;

namespace rowi_practice.Context;
public class DataBaseContext : DbContext
{
    public 
    DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) {}

    public DbSet<ExistingProblem> ExistingProblems { get; set; } = null!;
    public DbSet<Solution> Solutions { get; set; } = null!;
    public DbSet<Administartor> Administartors { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<SolutionToProblem> SolutionToProblems { get; set; } = null!;
}