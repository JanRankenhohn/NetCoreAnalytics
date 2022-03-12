using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreAnalytics
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AnalyticsController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Test()
        {
            return Ok();
        }
    }
}
