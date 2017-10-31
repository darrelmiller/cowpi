using cowpi.Services;
using System;
using System.Linq;
using System.Threading;
using Xunit;

namespace CowpiTests
{
    public class WorkerTests
    {
        [Fact]
        public void CreateAWorker()
        {
            var worker = new Worker();
            worker.AddWorkItem("dance", 4);

            Assert.NotEmpty(worker.WorkItems);
            Assert.Equal(4, worker.WorkItems.Last().Minutes);
            Assert.Equal("dance", worker.WorkItems.Last().Skill);
        }

        [Fact]
        public void WorkerIsWaiting()
        {
            var workItem = new WorkItem();
            
            Assert.Equal(StateEnum.Waiting,workItem.State);

        }
        [Fact]
        public void WorkerIsActive()
        {
            var worker = new Worker();
            var workItem = worker.AddWorkItem("dance", 4);

            Assert.Equal(StateEnum.Active, workItem.State);
            Assert.True(workItem.Completed > 0);

        }

        [Fact]
        public void WorkerIsCompleted()
        {
            var worker = new Worker();
            var workItem = worker.AddWorkItem("dance", 0.01M);
            Thread.Sleep(1000);
            Assert.Equal(StateEnum.Completed, workItem.State);

        }
    }
}
