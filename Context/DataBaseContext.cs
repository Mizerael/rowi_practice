using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using rowi_practice.Models;

namespace rowi_practice.Context;
public class DataBaseContext : DbContext
{
    public 
    DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) {}

    public DbSet<ExistingProblem> ExistingProblem { get; set; } = null!;
    public DbSet<Solution> Solution { get; set; } = null!;
    // public DbSet<Administartor> Administartor { get; set; } = null!;
    public DbSet<User> User { get; set; } = null!;
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {    
        optionsBuilder.UseMySQL("server=localhost;database=rowi_practice;user=rowi;password={159RoWi357}");
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}