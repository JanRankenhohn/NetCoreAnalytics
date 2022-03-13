using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAnalytics
{
    public class DataCollectionSingleton : IDataCollection
    {
        /* Singleton Pattern */

        private static readonly DataCollectionSingleton _dataCollection = new DataCollectionSingleton();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static DataCollectionSingleton()
        {
        }

        private DataCollectionSingleton()
        {
            LogDataLists = new();
        }

        public static DataCollectionSingleton DataCollection => _dataCollection;


        /* Data & Methods */

        private List<LogDataList> LogDataLists { get; set; }

        void IDataCollection.Update(LogData logData)
        {
            throw new NotImplementedException();
        }

        public long GetCurrentViewerCount(string name)
        {
            var list = LogDataLists.Find(l => l.Name == name);

            if (list  == null)
            {
                return 0;
            }

            if (list.Count == 0)
            {
                return 0;
            }

            return list.Last().CurrentViewCount;
        }

        public LogDataList Get(string name)
        {
            var list = LogDataLists.Find(l => l.Name == name);
            return list;
        }

        public List<LogDataList> GetAll()
        {
            return LogDataLists;
        }
    }
}
