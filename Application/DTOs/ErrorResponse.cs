using System.Net;

public class ErrorResponse
{
    public HttpStatusCode Code { get; set; }
    public string Message { get; set; }
    public dynamic Errors { get; set; }
}