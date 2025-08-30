using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmBeachApp.Data;

namespace SmBeachApp.Entities;

public static class ConfigureBaseModel
{
    public static void ConfigureBase<TEntity>(this EntityTypeBuilder<TEntity> entity) where TEntity : BaseModel
    {
        entity.Property(e => e.Reference)
            .HasColumnOrder(1)
            .IsRequired()
            .HasMaxLength(50)
            .HasDefaultValueSql("(host_name())");

        entity.Property(e => e.UserRef)
            .HasColumnOrder(2)
            .IsRequired()
            .HasMaxLength(50)
            .HasDefaultValueSql("(suser_sname())");

        entity.Property(e => e.DateChange)
            .HasColumnOrder(3)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");

        entity.Property(e => e.DateCreate)
            .HasColumnOrder(4)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
    }
}