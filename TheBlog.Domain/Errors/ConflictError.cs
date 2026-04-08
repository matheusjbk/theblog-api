using System.Net;

namespace TheBlog.Domain.Errors;

public class ConflictError : IError
{
    private readonly string _message;

    public ConflictError(string message)
    {
        _message = message;
    }

    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

    public IList<string> Messages => [_message];
}
