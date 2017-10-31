using cowpi.Representations;
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
        private int lastPeerId = 0;

        public int GetNextPeerId()
        {
            lock (this)
            {
                return ++lastPeerId;
            }
        }

        public ConcurrentDictionary<int, WorkerTask> CurrentTasks = new ConcurrentDictionary<int, WorkerTask>();

        public CooperatingWorker CooperatingWorker = new CooperatingWorker()
        {
            Skills = new List<string> { "sing", "dance" },
            TasksUrl = new Uri("tasks", UriKind.RelativeOrAbsolute),
            CapacityUrl = new Uri("capacity", UriKind.RelativeOrAbsolute)
        };

        public List<WorkItem> TotalCapacity = new List<WorkItem>() {
            new WorkItem() { Skill="sing", Minutes=10},
            new WorkItem() { Skill="dance", Minutes=20}
        };

        public ConcurrentDictionary<int, WorkerPeer> Peers = new ConcurrentDictionary<int, WorkerPeer>();


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
