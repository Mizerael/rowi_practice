using Microsoft.EntityFrameworkCore;

using rowi_practice.Models;

namespace rowi_practice.Context;

public class UserContext : DbContext
{
    public
    UserContext(DbContextOptions<UserContext> options) : base(options) {}
    
    public DbSet<User> User { get; set; } = null!;
}