using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Scalar.AspNetCore;
using TShip.ConfigurationBindings;
using TShip.Models.DTO.Wrappers;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
//builder.Services.AddControllers();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidateMetaFilter>();
});
builder.Services.AddOpenApi();

//DI (dependency injection) 
builder.Services
    .AddApplicationDbContext(builder.Configuration)
    .AddJwtConfig(builder.Configuration)
    .AddAccountBindings()
    .Configure<ApiBehaviorOptions>(
        options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var env = context.HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();
                if (env.IsDevelopment())
                {
                    // Dev trả lỗi chi tiết
                    //return new BadRequestObjectResult(context.ModelState);
                    return new BadRequestObjectResult(new
                    {
                        success = false,
                        message = "Dữ liệu không hợp lệ."
                    });
                }
                else
                {
                    // Prod trả lỗi chung
                    return new BadRequestObjectResult(new
                    {
                        success = false,
                        message = "Dữ liệu không hợp lệ."
                    });
                }
            };
        }
    );



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public class ValidateMetaFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var requestArg = context.ActionArguments.Values
            .FirstOrDefault(v =>
                v?.GetType().IsGenericType == true &&
                v.GetType().GetGenericTypeDefinition() == typeof(Request<>));

        if (requestArg == null)
            return; // Không áp dụng nếu action không nhận Request<>

        var meta = requestArg.GetType().GetProperty("Meta")?.GetValue(requestArg) as MetaData;
        var data = requestArg.GetType().GetProperty("Data")?.GetValue(requestArg);

        // Check meta/data có null không
        if (meta == null || data == null)
        {
            context.Result = new BadRequestObjectResult(new
            {
                success = false,
                message = "Dữ liệu không hợp lệ."
            });
            return;
        }

        // Check server name
        if (!string.Equals(meta.Server, "TSKD", StringComparison.OrdinalIgnoreCase))
        {
            context.Result = new BadRequestObjectResult(new
            {
                success = false,
                message = "Dữ liệu không hợp lệ."
            });
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Không cần xử lý sau khi action chạy
    }
}