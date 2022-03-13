using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAnalytics
{
    internal static class SessionAnalytics
    {
        private static readonly List<KeyValuePair<string, string>> _analyticsList
            = new List<KeyValuePair<string, string>>();

        public static List<KeyValuePair<string, string>> AnalyticsList => _analyticsList;
    }
}
