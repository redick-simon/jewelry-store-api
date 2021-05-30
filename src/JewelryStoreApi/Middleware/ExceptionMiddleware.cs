using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace JewelryStoreApi.Middleware
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;
        private ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch(Exception ex)
            {
                //writing to log
                _logger.LogError(ex.Message);

                var response = context.Response;
                response.ContentType = "application/json";

                var result = JsonSerializer.Serialize(new { message = "Interal Server Error Occured" });

                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await response.WriteAsync(result);
            }
        }
    }
}
