using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;
using TShip.ConfigurationBindings;
using TShip.Filters;
using TShip.Middleware;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
//builder.Services.AddControllers();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidateRequestFilter>();
});
builder.Services.AddOpenApi();

//DI (dependency injection) 
builder.Services
    .AddApplicationDbContext(builder.Configuration)
    .AddConfig(builder.Configuration)
    .AddRepoBindings()
    .AddServiceBindings()
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

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
