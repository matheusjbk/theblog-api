using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheBlog.Domain.Entities;

namespace TheBlog.Infra.DataAccess.ModelConfiguration;

public class UserConfiguration : EntityBaseConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasIndex(user => user.Email).IsUnique();

        base.Configure(builder);
        
        builder.Property(user => user.Name).IsRequired().HasMaxLength(255);
        builder.Property(user => user.Email).IsRequired().HasMaxLength(255);
        builder.Property(user => user.Password).IsRequired().HasMaxLength(2000);
    }
}
