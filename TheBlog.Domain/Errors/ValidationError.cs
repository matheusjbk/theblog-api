using System.Net;

namespace TheBlog.Domain.Errors;

public class ValidationError : IError
{
    private readonly IList<string> _messages;

    public ValidationError(IList<string> messages) => _messages = messages;

    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public IList<string> Messages => _messages;
}
