using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cowpi.Services
{
    public class Peer
    {
        public List<string> Skills { get; set; }
        public Uri TasksUrl { get; set; }
        public Uri CapacityUrl { get; set; }
    }
}
