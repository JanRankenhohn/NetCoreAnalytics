using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAnalytics
{
    public interface IDataCollection
    {
        internal void Update(LogData logData);

        public List<LogDataList> GetAll();

        public LogDataList Get(string name);

        public long GetCurrentViewerCount(string name);
    }
}
