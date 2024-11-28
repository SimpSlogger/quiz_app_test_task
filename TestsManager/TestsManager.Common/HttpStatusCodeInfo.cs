using System.Net;

namespace TestsManager.Common;

public class HttpStatusCodeInfo
{
    public HttpStatusCode Code { get; }
    public string Message { get; }

    public HttpStatusCodeInfo(HttpStatusCode code, string message)
    {
        Code = code;
        Message = message;
    }

    public static HttpStatusCodeInfo Create(HttpStatusCode code, string message)
    {
        return new HttpStatusCodeInfo(code, message);
    }
}
