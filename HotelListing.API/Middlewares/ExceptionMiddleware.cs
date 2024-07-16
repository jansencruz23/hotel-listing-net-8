using HotelListing.API.Models;
using HotelListing.Application.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace HotelListing.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            CustomProblemDetails problemDetails;

            switch (ex)
            {
                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    problemDetails = new CustomProblemDetails
                    {
                        Title = validationException.Message,
                        Status = (int)statusCode,
                        Detail = validationException.InnerException?.Message,
                        Type = nameof(ValidationException),
                        Errors = validationException.Errors
                    };
                    break;

                case NotFoundException notFound:
                    statusCode = HttpStatusCode.NotFound;
                    problemDetails = new CustomProblemDetails
                    {
                        Title = notFound.Message,
                        Status = (int)statusCode,
                        Type = nameof(NotFoundException),
                        Detail = notFound.InnerException?.Message,
                    };
                    break;

                case BadRequestException badRequest:
                    statusCode = HttpStatusCode.BadRequest;
                    problemDetails = new CustomProblemDetails
                    {
                        Title = badRequest.Message,
                        Status = (int)statusCode,
                        Type = nameof(BadRequestException),
                        Detail = badRequest.InnerException?.Message,
                    };
                    break;

                case UnauthorizedAccessException exception:
                    statusCode = HttpStatusCode.Unauthorized;
                    problemDetails = new CustomProblemDetails
                    {
                        Title = exception.Message,
                        Status = (int)statusCode,
                        Type = nameof(UnauthorizedAccessException),
                        Detail = exception.InnerException?.Message
                    };
                    break;

                default:
                    problemDetails = new CustomProblemDetails
                    {
                        Title = ex.Message,
                        Status = (int)statusCode,
                        Type = nameof(HttpStatusCode.InternalServerError),
                        Detail = ex.StackTrace,
                    };
                    break;
                }

            _logger.LogError($"An error occured. Status code: {statusCode}. Title: {problemDetails.Title}. Message: {problemDetails.Detail}.");
            httpContext.Response.StatusCode = (int)statusCode;
            await httpContext.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
