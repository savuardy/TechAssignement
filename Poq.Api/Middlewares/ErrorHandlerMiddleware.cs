using Poq.DataSourceClient.Exceptions;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Poq.Api.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case MockyUnavailableException e:
                        response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                        break;
                    case ArgumentException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        var bytes = Encoding.UTF8.GetBytes(e.Message);
                        await response.Body.WriteAsync(bytes, 0, bytes.Length);
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
