using System.Net;

namespace TheBlog.Domain.Errors;

public class UnauthorizedError : IError
{
    private readonly string _message;

    public UnauthorizedError(string message) => _message = message;

    public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

    public IList<string> Messages => [_message];
}
