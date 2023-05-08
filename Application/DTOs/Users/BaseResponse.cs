namespace Application.DTOs
{
    public interface BaseResponse<T>
    {
        T? data { get; set; }
        int code { get; set; }
        string? message { get; set; }
    }
}

