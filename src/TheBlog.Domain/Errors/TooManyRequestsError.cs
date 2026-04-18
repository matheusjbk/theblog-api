using System.Net;

namespace TheBlog.Domain.Errors;

public class TooManyRequestsError : IError
{
    private readonly string _message;

    public TooManyRequestsError(string message) => _message = message;

    public HttpStatusCode StatusCode => HttpStatusCode.TooManyRequests;

    public IList<string> Messages => [_message];
}
