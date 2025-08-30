using Mch.Authentication.ContextDb.Models;
using Mch.ContextDbBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mch.Authentication.ContextDb.Configurations
{
    public class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> entity)
        {
            entity.HasKey(e => new { e.UserId, e.GroupId }).HasName("PK_UserGroup");

            entity.ToTable("UserGroups");

            entity.HasIndex(e => e.UserId, "IX_UserGroup_UserId");

            entity.HasIndex(e => e.GroupId, "IX_UserGroup_GroupId");

            entity.HasOne(d => d.User).WithMany(p => p.UserGroups)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_UserGroup_User");

            entity.HasOne(d => d.Group).WithMany(p => p.UserGroups)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_UserGroup_Group");

            // Apply Base Configuration
            entity.ConfigureBase();
        }
    }
}
