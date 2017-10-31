using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace cowpi.Representations
{
    public class CooperatingWorkers :  List<CooperatingWorker>
    {

    }

    public class CooperatingWorker
    {
        public Uri TasksUrl { get; set; }
        public Uri CapacityUrl { get; set; }

        public List<string> Skills { get; set; } = new List<string>();

        internal static CooperatingWorker Load(StreamReader sr)
        {
            return new CooperatingWorker();
        }
    }
}
