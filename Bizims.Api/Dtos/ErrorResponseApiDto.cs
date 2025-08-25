 namespace Bizims.Api;

public class ErrorResponseApiDto
{
    public required string Message { get; set; }
    public required string Title { get; set; }
    public int StatusCode { get; set; }
    public required string StatusMessage { get; set; }
}