using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TShip.Models.DTO.Wrappers;

namespace TShip.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var meta = context.ActionArguments.Values
                    .OfType<dynamic>()
                    .FirstOrDefault()?.Meta;

                context.Result = new OkObjectResult(new Response<object>
                {
                    Meta = meta,
                    Success = false,
                    Message = "Thông tin không hợp lệ.",
                    Data = null
                });
            }
        }
    }
}
