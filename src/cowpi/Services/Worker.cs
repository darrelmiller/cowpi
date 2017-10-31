using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cowpi.Services
{
    public class Worker
    {
        private int lastTaskId = 0;

        public int GetNextTaskId()
        {
            lock(this)
            {
                return ++lastTaskId;
            }
        }

        public ConcurrentDictionary<int, WorkerTask> CurrentTasks = new ConcurrentDictionary<int, WorkerTask>();

        public List<WorkItem> TotalCapacity = new List<WorkItem>() {
            new WorkItem() { Skill="sing", Minutes=10},
            new WorkItem() { Skill="dance", Minutes=20}
        };


        public List<WorkItem> WorkItems { get; private set; } = new List<WorkItem>();

        public WorkItem AddWorkItem(string skill, decimal minutes)
        {
            var wi = new WorkItem() {
                Skill  = skill,
                Minutes = minutes
            };
            WorkItems.Add(wi);
            wi.Start();
            return wi;
        }
    }

    public enum StateEnum
    {
        Waiting,
        Active,
        Completed,
        Cancelled
    }

}
