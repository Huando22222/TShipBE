using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TShip.Models.DTO.Wrappers;

namespace TShip.Middleware
{
    public class MetaValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public MetaValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Đọc body
            context.Request.EnableBuffering();
            var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
            context.Request.Body.Position = 0;

            if (!string.IsNullOrWhiteSpace(body))
            {
                try
                {
                    using var doc = JsonDocument.Parse(body);
                    if (doc.RootElement.TryGetProperty("meta", out var metaProp))
                    {
                        var serverValue = metaProp.GetProperty("server").GetString();
                        if (serverValue != "TShip")
                        {
                            context.Response.StatusCode = StatusCodes.Status400BadRequest;
                            context.Response.ContentType = "application/json";
                            await context.Response.WriteAsync(JsonSerializer.Serialize(new
                            {
                                success = false,
                                message = "Dữ liệu không hợp lệ."
                            }));
                            return;
                        }
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            success = false,
                            message = "Dữ liệu không hợp lệ."
                        }));
                        return;
                    }
                }
                catch
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        success = false,
                        message = "Dữ liệu không hợp lệ."
                    }));
                    return;
                }
            }

            await _next(context);
        }
    }
}
