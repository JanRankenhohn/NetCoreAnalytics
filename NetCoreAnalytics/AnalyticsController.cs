using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace NetCoreAnalytics
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AnalyticsController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMemoryCache _memoryCache;
        private readonly IDataCollection _dataCollection;
        private readonly string _instanceName;

        public AnalyticsController(IHttpContextAccessor httpContextAccessor, IMemoryCache memoryCache, IDataCollection dataCollection)
        {
            _httpContextAccessor = httpContextAccessor;
            _memoryCache = memoryCache;
            _dataCollection = dataCollection;
            _instanceName = System.Environment.MachineName;
        }

        [HttpPost]
        public async Task<JsonResult> AnalyticsService(string name)
        {
            string? userSessionId = _httpContextAccessor.HttpContext?.Session.Id;

            if (string.IsNullOrEmpty(userSessionId))
            {
                return new JsonResult(new
                {
                    status = "Failure",
                    message = "Context, session or session id is null."
                });
            }

            if (string.IsNullOrEmpty(name))
            {
                return new JsonResult(new
                {
                    status = "Failure",
                    message = $"No name for Data Collection defined."
                });
            }

            long lastFullMinute = DateTimeOffset.Now.ToUnixTimeSeconds() / 60 * 60;
            string currentAnalyticsKey = "analytics_" + name + "_" + lastFullMinute;
            string appSesionKey = "analytics_" + name + "_timestamp";
            string _dbRequestRunningKey = "analytics_" + name + "_dbrequestrunning";

            if (!SessionAnalytics.AnalyticsList.Any(a => a.Key == userSessionId && a.Value == currentAnalyticsKey))
            {
                SessionAnalytics.AnalyticsList.Add(new KeyValuePair<string, string>(userSessionId, currentAnalyticsKey));
            }

            long? lastTimeStamp = _memoryCache.Get<long?>(appSesionKey);

            if (lastTimeStamp != null && lastFullMinute > lastTimeStamp)
            {
                bool dbRequestRunning = _memoryCache.Get<bool>(_dbRequestRunningKey);

                if (!dbRequestRunning)
                {
                    _memoryCache.Set(_dbRequestRunningKey, true);
                    try
                    {
                        string oldAnalyticsKey = name + "_" + lastTimeStamp;
                        var analyticsList = SessionAnalytics.AnalyticsList.Where(a => a.Value == oldAnalyticsKey);

                        _dataCollection.Update(new LogData
                        {
                            Name = name,
                            Instance = _instanceName,
                            Timestamp = (long)lastTimeStamp
                        });

                        SessionAnalytics.AnalyticsList.RemoveAll(a => a.Value == oldAnalyticsKey);
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        _memoryCache.Set(_dbRequestRunningKey, false);
                    }
                }
            }
            _memoryCache.Set(appSesionKey, lastFullMinute);

            return new JsonResult(new { status = "success" });
        }
    }
}
