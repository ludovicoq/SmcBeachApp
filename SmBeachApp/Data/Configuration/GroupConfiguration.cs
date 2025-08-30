using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmBeachApp.Entities;

namespace SmBeachApp.Data.Configuration
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> entity)
        {
            entity.HasKey(e => e.GroupId).HasName("PK_Group");

            entity.ToTable("Groups");

            entity.HasIndex(e => e.Name, "UIX_Group_Name").IsUnique();

            entity.Property(e => e.GroupId)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Apply Base Configuration
            entity.ConfigureBase();
        }
    }
}
