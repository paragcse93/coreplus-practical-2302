namespace Coreplus.Sample.Api.ResponseModel;


public class APIResponse<T> where T : class
{
    public int statusCode { get; set; }
    public string message { get; set; }
    public T data { get; set; }

}