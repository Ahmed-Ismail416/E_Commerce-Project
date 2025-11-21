using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Attributes
{
    public class CacheAttribute(int duration) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Create Cache Key
            var cacheKey = GetCacheKey(context.HttpContext.Request);
            // Search with the key in cache
            ICacheService cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var cacheValue = await cacheService.GetAsync(cacheKey);
            if ( cacheValue != null)
            {
                context.Result = new ContentResult()
                {
                    Content = cacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK

                };
                return;
            }
            // invoke the action
            var executedContext = await next.Invoke();
            //Check if the action result is ok
            if (executedContext.Result is ObjectResult objectResult)
            {
                await cacheService.SetAsync(cacheKey, objectResult.Value, TimeSpan.FromMinutes(duration));
            }
        }

        private string GetCacheKey(HttpRequest request)
        {
            StringBuilder Key = new StringBuilder();
            Key.Append(request.Path+'?');
            foreach (var item in request.Query.OrderBy(o => o.Key))
            {
                Key.Append($"{item.Key}={item.Value}&");

            }
            return Key.ToString();
        }
    }
}
