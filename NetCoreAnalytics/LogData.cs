using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAnalytics
{
    public class LogData
    {
        public string Name { get; set; }
        public string Instance { get; set; }
        public int CurrentViewCount { get; set; }
        public long Timestamp { get; set; }
    }
}
