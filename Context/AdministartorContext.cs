using Microsoft.EntityFrameworkCore;

using rowi_practice.Models;

namespace rowi_practice.Context;

public class AdministartorContext : DbContext
{
    public
    AdministartorContext(DbContextOptions<AdministartorContext> options) : base(options) {}

    public DbSet<Administartor> Administartor { get; set; } = null!;
}