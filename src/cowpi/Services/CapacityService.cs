using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cowpi.Representations;

namespace cowpi.Services
{
    public class CapacityService
    {
        private Worker worker;

        public CapacityService(Worker worker)
        {
            this.worker = worker;
        }

        internal StatementOfWork GetCapacity()
        {
            var sow = new StatementOfWork();
            var task = new StatementOfWork.Task
            {
                EstimatedWork = worker.TotalCapacity.Select(wi => new StatementOfWork.WorkItem() { Skill = wi.Skill, Minutes = wi.Minutes }).ToList()
            };

            sow.Add(task);

            return sow;
        }
    }
}
