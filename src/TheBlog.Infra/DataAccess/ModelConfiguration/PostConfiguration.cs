using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheBlog.Domain.Entities;

namespace TheBlog.Infra.DataAccess.ModelConfiguration;

public class PostConfiguration : EntityBaseConfiguration<Post>
{
    public override void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");
        builder.HasOne(post => post.Author).WithMany().HasForeignKey(post => post.AuthorId);
        builder.HasIndex(post => post.Slug).IsUnique();

        base.Configure(builder);

        builder.Property(post => post.Title).IsRequired().HasMaxLength(150);
        builder.Property(post => post.Slug).IsRequired();
        builder.Property(post => post.Excerpt).IsRequired().HasMaxLength(200);
        builder.Property(post => post.Content).IsRequired();
        builder.Property(post => post.CoverImageUrl).IsRequired();
    }
}
