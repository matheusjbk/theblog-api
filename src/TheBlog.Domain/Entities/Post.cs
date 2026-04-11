namespace TheBlog.Domain.Entities;

public class Post : EntityBase
{
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Excerpt { get; set; } = string.Empty;
    public string CoverImageUrl { get; set; } = string.Empty;
    public Guid AuthorId { get; set; }

    public User Author { get; set; } = default!;
}
