namespace Application.DTOs
{
    public class BaseResponse<T>
    {
        public T? data { get; set; }
        public int code { get; set; }
        public string? message { get; set; }
    }
}

