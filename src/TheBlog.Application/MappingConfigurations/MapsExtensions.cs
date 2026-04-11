using TheBlog.Application.Communication.Requests;
using TheBlog.Application.Communication.Responses;
using TheBlog.Domain.Entities;

namespace TheBlog.Application.MappingConfigurations;

public static class MapsExtensions
{
    // Request to Entity
    public static User ToUserEntity(this RegisterUserRequest request)
    {
        return new User
        {
            Name = request.Name,
            Email = request.Email
        };
    }

    public static Post ToPostEntity(this PostRequest request)
    {
        return new Post
        {
            Title = request.Title,
            Excerpt = request.Excerpt,
            Content = request.Content,
            CoverImageUrl = request.CoverImageUrl,
            Active = request.Active,
        };
    }

    // Entity to Response
    public static RegisteredUserResponse ToRegisteredUserResponse(this User user)
    {
        return new RegisteredUserResponse
        {
            Name = user.Name,
            Email = user.Email
        };
    }

    public static PostResponse ToPostResponse(this Post post)
    {
        return new PostResponse
        {
            Id = post.Id,
            Title = post.Title,
            Slug = post.Slug,
            Excerpt = post.Excerpt,
            Content = post.Content,
            CoverImageUrl = post.CoverImageUrl,
            Active = post.Active,
        };
    }
}
