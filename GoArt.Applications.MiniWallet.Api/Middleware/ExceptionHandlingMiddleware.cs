using System.Net;
using System.Text.Json;
using GoArt.Applications.MiniWallet.Api.Utilties;
using GoArt.Applications.MiniWallet.Core.Problem;
using GoArt.Applications.MiniWallet.Localization;

namespace GoArt.Applications.MiniWallet.Api.Middleware;

internal sealed class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    private readonly ILocalizer _localizer;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger, ILocalizer localizer)
    {
        _localizer = localizer;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        bool isValidationException = exception is ProblemException;
        httpContext.Response.ContentType = "application/json";

        //string? langCode = httpContext.Request.Headers.AcceptLanguage.FirstOrDefault();
        string langCode = AcceptLanguageHeaderParser.Parse(httpContext.Request.Headers.AcceptLanguage.FirstOrDefault()! ,"en");

        string instance = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.PathBase}{httpContext.Request.Path}{httpContext.Request.QueryString}";

        if (isValidationException)
        {
            Problem problem = ((ProblemException)exception).Problem;
            problem.Title = this._localizer.Localize(langCode, problem.Type + "_TITLE");
            problem.Detail = this._localizer.Localize(langCode, problem.Type + "_DETAIL");
            problem.Instance = instance;
            problem.Status = problem.Status is null ? (int)HttpStatusCode.BadRequest : (int)problem.Status;

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(problem));
            return;
        }

        var response = new
        {
            Title = this._localizer.Localize(langCode, "GENERIC_EXCEPTION_TITLE"),
            Detail = this._localizer.Localize(langCode, "GENERIC_EXCEPTION_DETAIL"),
            StatusCode = StatusCodes.Status500InternalServerError,
            Instance = instance
        };
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize((response)));
    }
}