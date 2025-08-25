using Microsoft.AspNetCore.Mvc;

namespace Bizims.Api;

public class SuccessResponseApiDto<TResult>
{
    public int StatusCode { get; set; }
    public string? SuccessMessage { get; set; }
    public TResult? Result { get; set; }
}

public class SuccessResponseApiDto
{
    public int StatusCode { get; set; }
    public string? SuccessMessage { get; set; }
}

public class SuccessResult : IActionResult
{
    private readonly SuccessResponseApiDto _response;

    private SuccessResult(int statusCode, string? successMessage)
    {
        _response = new SuccessResponseApiDto
        {
            StatusCode = statusCode,
            SuccessMessage = successMessage
        };
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        var response = context.HttpContext.Response;

        response.StatusCode = _response.StatusCode;
        response.ContentType = "application/json";

        await response.WriteAsJsonAsync(_response);
    }
    public static SuccessResult Create(int statusCode, string? successMessage = null)
    {
        return new SuccessResult(statusCode, successMessage);
    }

    public static SuccessResult<TResult> Create<TResult>(int statusCode, TResult result, string? successMessage = null)
    {
        return new SuccessResult<TResult>(statusCode, successMessage, result);
    }
}

public class SuccessResult<TResult> : IActionResult
{
    private readonly SuccessResponseApiDto<TResult> _response;

    public SuccessResult(int statusCode, string? successMessage, TResult result)
    {
        _response = new SuccessResponseApiDto<TResult>
        {
            StatusCode = statusCode,
            SuccessMessage = successMessage,
            Result = result
        };
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        var response = context.HttpContext.Response;

        response.StatusCode = _response.StatusCode;
        response.ContentType = "application/json";

        await response.WriteAsJsonAsync(_response);
    }
}