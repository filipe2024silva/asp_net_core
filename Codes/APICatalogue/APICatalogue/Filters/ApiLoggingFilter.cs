using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogue.Filters
{
    public class ApiLoggingFilter : IActionFilter
    {
        private readonly ILogger<ApiLoggingFilter> _logger;

        public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //execute before action is called
            _logger.LogInformation($"### Executing -> OnActionExecuting");
            _logger.LogInformation($"###################################################");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation($"ModelState: {context.ModelState.IsValid}");
            _logger.LogInformation($"###################################################");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //execute after action is executed
            _logger.LogInformation($"### Executing -> OnActionExecuted");
            _logger.LogInformation($"###################################################");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation($"Status Code: {context.HttpContext.Response.StatusCode}");
            _logger.LogInformation($"###################################################");
        }
    }
}
