namespace TheBlog.Application.Communication.Requests;

public class PostRequest
{
    public string Title { get; set; } = string.Empty;
    public string Excerpt { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string CoverImageUrl { get; set; } = string.Empty;
    public bool Active { get; set; } = true;
}
