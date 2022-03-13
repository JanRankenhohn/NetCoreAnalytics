using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace NetCoreAnalytics
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddNetCoreAnalytics(this IServiceCollection services)
        {
            services.AddControllers();
            if (!services.Any(x => x.ServiceType == typeof(IHttpContextAccessor)))
            {
                services.AddHttpContextAccessor();
            }
            if (!services.Any(x => x.ServiceType == typeof(IMemoryCache)))
            {
                services.AddMemoryCache();
            }
            
            return services;
        }
    }

    //[ApiController]
    //[Route("[controller]")]
    //public class UsersController : ControllerBase
    //{
    //    [HttpGet]
    //    public async Task<ActionResult> Test()
    //    {
    //        return Ok();
    //    }
    //}
}
