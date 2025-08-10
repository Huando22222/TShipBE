using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TShip.Models.DTO.Wrappers;

namespace TShip.Filters
{
    public class ValidateRequestFilter : IAsyncActionFilter
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
}
