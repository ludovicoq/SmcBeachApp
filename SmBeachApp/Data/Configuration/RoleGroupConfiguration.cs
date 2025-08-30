using Mch.Authentication.ContextDb.Models;
using Mch.ContextDbBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mch.Authentication.ContextDb.Configurations.Authentication
{
    public class RoleGroupConfiguration : IEntityTypeConfiguration<RoleGroup>
    {
        public void Configure(EntityTypeBuilder<RoleGroup> entity)
        {
            entity.HasKey(e => new { e.RoleId, e.GroupId }).HasName("PK_RoleGroup");

            entity.ToTable("RoleGroups");

            entity.HasIndex(e => e.RoleId, "IX_RoleGroup_RoleId");

            entity.HasIndex(e => e.GroupId, "IX_RoleGroup_GroupId");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleGroups)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_RoleGroup_Role");

            entity.HasOne(d => d.Group).WithMany(p => p.RoleGroups)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_RoleGroup_Group");

            // Apply Base Configuration
            entity.ConfigureBase();
        }
    }
}
