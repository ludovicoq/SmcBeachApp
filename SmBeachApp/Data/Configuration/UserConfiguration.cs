using Mch.Authentication.ContextDb.Models;
using Mch.ContextDbBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mch.Authentication.ContextDb.Configurations.Authentication
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasKey(e => e.UserId).HasName("PK_User");

            entity.ToTable("Users");

            entity.HasIndex(e => e.UserName, "UIX_User_UserName").IsUnique();

            entity.Property(e => e.UserId)
                .HasColumnOrder(0)
                .HasDefaultValueSql("(newsequentialid())");

            entity.Property(e => e.Email).HasMaxLength(50);

            entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(250);

            entity.Property(e => e.UserName)
               .IsRequired()
               .HasMaxLength(50);

            // Apply Base Configuration
            entity.ConfigureBase();
        }
    }
}
