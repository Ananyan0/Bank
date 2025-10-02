using Bank.Application.Exceptions;

namespace Bank.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger,
            IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var statusCode = ex switch
                {
                    CustomerException => StatusCodes.Status404NotFound,
                    AccountException => StatusCodes.Status404NotFound,
                    BranchException bex => bex.StatusCode
                    //_ => StatusCodes.Status500InternalServerError
                };

                context.Response.StatusCode = statusCode;


                var response = new
                {
                    message = ex.Message,
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
