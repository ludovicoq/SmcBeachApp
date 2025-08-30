using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmBeachApp.Entities;

namespace SmBeachApp.Data.Configuration;

public static class ConfigureBaseModel
{
    public static void ConfigureBase<TEntity>(this EntityTypeBuilder<TEntity> entity) where TEntity : BaseModel
    {
        entity.Property(e => e.Reference)
            .HasColumnOrder(1)
            .IsRequired()
            .HasMaxLength(50)
            .HasDefaultValue("");

        entity.Property(e => e.UserRef)
            .HasColumnOrder(2)
            .IsRequired()
            .HasMaxLength(50)
            .HasDefaultValue("SYSTEM_USER");

        entity.Property(e => e.DateChange)
            .HasColumnOrder(3)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("datetime");

        entity.Property(e => e.DateCreate)
            .HasColumnOrder(4)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("datetime");

    }
}