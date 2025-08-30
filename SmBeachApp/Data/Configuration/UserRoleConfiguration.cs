using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmBeachApp.Entities;

namespace SmBeachApp.Data.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> entity)
        {
            entity.HasKey(e => new { e.UserId, e.RoleId }).HasName("PK_UserRole");

            entity.ToTable("UserRoles");

            entity.HasIndex(e => e.RoleId, "IX_UserRole_RoleId");

            entity.HasIndex(e => e.UserId, "IX_UserRole_UserId");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_UserRole_Role");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_UserRole_User");

            // Apply Base Configuration
            entity.ConfigureBase();
        }
    }
}
