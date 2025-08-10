using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Scalar.AspNetCore;
using TShip.ConfigurationBindings;
using TShip.Filters;
using TShip.Middleware;
using TShip.Models.DTO.Wrappers;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
//builder.Services.AddControllers();
builder.Services.AddControllers(options =>
{
    //options.Filters.Add<ValidateMetaFilter>();
    options.Filters.Add<ValidateMetaFilter>();
});
builder.Services.AddOpenApi();

//DI (dependency injection) 
builder.Services
    .AddApplicationDbContext(builder.Configuration)
    .AddJwtConfig(builder.Configuration)
    .AddAccountBindings()
    .Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    })
    ;



var app = builder.Build();

app.UseMiddleware<MetaValidationMiddleware>();

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

public class ValidateMetaFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ActionArguments.Count == 0)
        {
            context.Result = new BadRequestObjectResult(new
            {
                success = false,
                message = "không hợp lệ."
            });
            return;
        }

        var requestObj = context.ActionArguments.Values.FirstOrDefault();
        var metaProp = requestObj?.GetType().GetProperty("Meta")?.GetValue(requestObj);

        var serverValue = metaProp?.GetType().GetProperty("Server")?.GetValue(metaProp)?.ToString();
        if (metaProp == null || !string.Equals(serverValue, "TShip", StringComparison.OrdinalIgnoreCase))
        {
            context.Result = new BadRequestObjectResult(new
            {
                success = false,
                message = "không hợp lệ."
            });
            return;
        }

        // Nếu ModelState không hợp lệ (Data invalid)
        if (!context.ModelState.IsValid)
        {
            context.Result = new OkObjectResult(new Response<object?>
            {
                Meta = (MetaData)metaProp,
                Success = false,
                Message = "request không hợp lệ.",
                Data = null
            });
            return;
        }

        await next();
    }
}
