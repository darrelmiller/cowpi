using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cowpi.Representations;

namespace cowpi.Services
{
    public class PeerService
    {

        private Worker worker;

        public PeerService(Worker worker)
        {
            this.worker = worker;
        }


        internal CooperatingWorkers GetPeers()
        {
            var cows = new CooperatingWorkers();

            foreach (var workerPeer in this.worker.Peers.Values)
            {
                cows.Add(new CooperatingWorker()
                {
                    Skills = workerPeer.Skills,
                    TasksUrl = workerPeer.TasksUrl,
                    CapacityUrl = workerPeer.CapacityUrl
                });
            }

            return cows;
        }

        internal WorkerPeer GetPeer(int id)
        {
            WorkerPeer peer = null;
            if (this.worker.Peers.TryGetValue(id, out peer))
            {
                return peer;
            }
            else
            {
                return null;
            }

        }


        internal WorkerPeer CreatePeer(CooperatingWorker cow)
        {
                var workerPeer = new WorkerPeer
                {
                    Id = worker.GetNextTaskId(),
                    TasksUrl = cow.TasksUrl,
                    CapacityUrl = cow.CapacityUrl
                };


            if (this.worker.Peers.TryAdd(workerPeer.Id, workerPeer))
            {
                return workerPeer;
            }
            else
            {
                return null;
            }

        }

        internal WorkerPeer Delete(int id)
        {

            WorkerPeer peer = null;
            if (this.worker.Peers.TryRemove(id, out peer))
            {
                return peer;
            }
            else
            {
                return null;
            }

        }
    }
}
