using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmBeachApp.Entities;

namespace SmBeachApp.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasKey(e => e.UserId).HasName("PK_User");

            entity.ToTable("Users");

            entity.HasIndex(e => e.Username, "UIX_User_Username").IsUnique();

            entity.Property(e => e.UserId)
                .HasColumnOrder(0)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Email).HasMaxLength(50);

            entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(250);

            entity.Property(e => e.Username)
               .IsRequired()
               .HasMaxLength(50);

            // Apply Base Configuration
            entity.ConfigureBase();
        }
    }
}
