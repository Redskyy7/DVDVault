using System.Net;

namespace DVDVault.Domain.Interfaces.Abstractions;
public interface IResponse
{
    HttpStatusCode StatusCode { get; set; }
    string Message { get; set; }
    Dictionary<string, string> Errors { get; set; }

}
