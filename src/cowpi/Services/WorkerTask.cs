using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cowpi.Services
{
    public class WorkerTask
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<string> RequiredSkills { get; set; }
        public List<WorkItem> WorkItems { get; set; }
        public DateTime? DueDateTime { get; set; } = null;

        internal string GetStatus()
        {
            if (WorkItems.All(wi => wi.State == StateEnum.Completed)) {
                return "Completed";
            }
            if (WorkItems.Any(wi => wi.State == StateEnum.Active))
            {
                return "Active";
            }
            if (WorkItems.All(wi => wi.State == StateEnum.Waiting))
            {
                return "Waiting";
            }
            if (WorkItems.All(wi => wi.State == StateEnum.Cancelled))
            {
                return "Cancelled";
            }
            return "Unknown";
        }
    }
}
