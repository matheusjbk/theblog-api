using System.Net;

namespace TheBlog.Domain.Errors;

public interface IError
{
    public HttpStatusCode StatusCode { get; }
    public IList<string> Messages { get; }
}
