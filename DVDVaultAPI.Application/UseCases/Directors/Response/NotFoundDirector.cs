using DVDVault.Domain.Interfaces.Abstractions;
using System.Net;

namespace DVDVault.Application.UseCases.Directors.Response;
public class NotFoundDirector : IResponse
{
    public NotFoundDirector(HttpStatusCode StatusCode, string Message)
    {
        this.StatusCode = StatusCode;
        this.Message = Message;
    }

    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
    public Dictionary<string, string> Errors { get; set; }
}
