using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;

namespace NetCoreAnalytics
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddNetCoreAnalytics(this IServiceCollection services)
        {
            services.AddControllers();
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
