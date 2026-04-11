namespace TheBlog.Application.Communication.Responses;

public class PostResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Excerpt { get; set; } = string.Empty;
    public string CoverImageUrl { get; set; } = string.Empty;
    public bool Active { get; set; }
    public RegisteredUserResponse Author { get; set; } = default!;
}
