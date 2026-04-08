using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheBlog.Domain.Entities;

namespace TheBlog.Infra.DataAccess.ModelConfiguration;

public abstract class EntityBaseConfiguration<T> : IEntityTypeConfiguration<T> where T: EntityBase
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.UpdatedAt).IsRequired();
        builder.Property(e => e.Active).IsRequired();
    }
}
