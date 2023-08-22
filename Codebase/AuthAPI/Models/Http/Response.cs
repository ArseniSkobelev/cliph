namespace cliph.Models.Http;

public class Response<T>
{
    public Response()
    {
    }

    public Response(bool success)
    {
        Success = success;
    }

    public Response(bool success, string message)
    {
        Success = success;
        Message = message;
    }

    public Response(bool success, T data)
    {
        Success = success;
        Data = data;
    }

    public Response(bool success, T data, string message)
    {
        Data = data;
        Success = success;
        Message = message;
    }

    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
}

public class Response
{
    public Response(bool success)
    {
        Success = success;
    }

    public Response(bool success, string message)
    {
        Success = success;
        Message = message;
    }

    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}