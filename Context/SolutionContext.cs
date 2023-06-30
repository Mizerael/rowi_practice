using Microsoft.EntityFrameworkCore;

using rowi_practice.Models;

namespace rowi_practice.Context;

public class SolutionContext : DbContext
{
    public
    SolutionContext(DbContextOptions<SolutionContext> options) : base(options) {}
    
    public DbSet<Solution> Solution { get; set; } = null!;
}