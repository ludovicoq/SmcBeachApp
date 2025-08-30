using Microsoft.EntityFrameworkCore;
using SmBeachApp.Entities;

namespace SmBeachApp.Data;

[ActivatorUtilitiesConstructor]
public class SmBeachDbContext(DbContextOptions<SmBeachDbContext> options) : DbContext(options)
{
    public virtual DbSet<Group> Groups { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<RoleGroup> RoleGroups { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserGroup> UserGroups { get; set; }
    public virtual DbSet<UserRole> UserRoles { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SmBeachDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}