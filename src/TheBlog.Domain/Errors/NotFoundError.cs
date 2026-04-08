using System.Net;

namespace TheBlog.Domain.Errors;

public class NotFoundError : IError
{
    private readonly string _message;

    public NotFoundError(string message) => _message = message;

    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public IList<string> Messages => [_message];
}
