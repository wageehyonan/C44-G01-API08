using Ecommerce_G02.Abstractions.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Presentation.Attributes
{
    public class CacheAttribute(int TimeLiveInSecond=90):ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string CahceKey = CreateCaheKey(context.HttpContext.Request);

            ICacheServices Cacheservice = context.HttpContext.RequestServices.GetRequiredService<ICacheServices>();

            var CacheValue=await Cacheservice.GetCacheAsync(CahceKey);
            if (CacheValue != null)
            {
                context.Result = new ContentResult()
                {
                    Content = CacheValue,
                    ContentType = "application/jason",
                    StatusCode = StatusCodes.Status200OK

                };
                return;
            }

           
                var ExcutedContext=await next.Invoke();

                if (ExcutedContext.Result is ObjectResult result)
                {
                    await Cacheservice.SetCacheAsync(CahceKey, result.Value,TimeSpan.FromSeconds(TimeLiveInSecond));
                }
            
        }
        private string CreateCaheKey(HttpRequest request)

        {
            StringBuilder  key= new StringBuilder();

            key.Append(request.Path + '?');

            foreach (var item in request.Query.OrderBy(o=>o.Key))
            {
                key.Append($"{item.Key}={item.Value}&");
            }
            return key.ToString();
        }
    }
}
