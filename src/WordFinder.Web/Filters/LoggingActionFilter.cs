//using Microsoft.AspNetCore.Mvc.Filters;

//namespace WordFinder.Web.Filters;

//internal sealed class LoggingActionFilter : IAsyncActionFilter
//{
//    private readonly ILogger _logger;

//    public LoggingActionFilter(ILogger logger)
//    {
//        _logger = logger;
//    }

//    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
//    {
//        _logger.LogInformation("BEFORE");
//        await next();
//        _logger.LogInformation("AFTER");
//    }
//}
