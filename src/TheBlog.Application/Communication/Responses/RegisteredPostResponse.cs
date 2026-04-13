namespace TheBlog.Application.Communication.Responses;

public class RegisteredPostResponse
{
    public Guid Id {  get; set; }
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}
