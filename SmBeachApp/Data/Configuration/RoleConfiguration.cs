using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmBeachApp.Entities;

namespace SmBeachApp.Data.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> entity)
        {
            entity.HasKey(e => e.RoleId).HasName("PK_Role");

            entity.ToTable("Roles");

            entity.HasIndex(e => e.Name, "UIX_Role_Name").IsUnique();

            entity.Property(e => e.RoleId)
                .HasColumnOrder(0)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Apply Base Configuration
            entity.ConfigureBase();
        }
    }
}
