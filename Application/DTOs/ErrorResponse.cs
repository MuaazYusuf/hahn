using System.Net;

public class ErrorResponse
{
    public HttpStatusCode code { get; set; }
    public string message { get; set; }
    public dynamic errors { get; set; }
}