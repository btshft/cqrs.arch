using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Cqrs.Infrastructure.Exceptions;
using Cqrs.WebApi.Infrastructure.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Cqrs.WebApi.Infrastructure.ExceptionHandling
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostingEnvironment _environment;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, IHostingEnvironment environment, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _environment = environment;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Во время запроса произошло исключение (TradeId: {context.TraceIdentifier}; Path: {context.Request.Path})");
                
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var apiResponse = CreateApiError(context, exception);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            
            await context.Response.WriteAsync(
                JsonConvert.SerializeObject(apiResponse, Formatting.Indented), 
                Encoding.UTF8);
        }

        private ApiError CreateApiError(HttpContext context, Exception exception)
        {
            if (_environment.IsDevelopment())
            {
                return new ApiError(exception.Message, exception.ToString(), context.TraceIdentifier);
            }

            if (exception.TryGetUserException(out var userException))
            {
                return new ApiError(userException.Message, userException.Description, context.TraceIdentifier);
            }
            
            return new ApiError(
                "Произошло исключение", 
                "При обработке запроса произошла ошибка. Попробуйте выполнить запрос позже", 
                context.TraceIdentifier);
        }
    }
}