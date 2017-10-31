using System;
using System.Collections.Generic;
using System.Linq;
using cowpi.Representations;

namespace cowpi.Services
{
    public class TaskService
    {
        private Worker worker;
        
        
        public TaskService(Worker worker)
        {
            this.worker = worker;
        }

        internal List<WorkerTask> CreateTasks(StatementOfWork newSow)
        {
            var newTasks = new List<WorkerTask>();
            foreach (var task in newSow)
            {
                var workerTask = new WorkerTask
                {
                    Id = worker.GetNextTaskId(),
                    Description = task.Description,
                    RequiredSkills = task.RequiredSkills,
                    DueDateTime = task.DueDateTime,  
                };

                workerTask.WorkItems = new List<WorkItem>();
                foreach (var wi in task.EstimatedWork)
                {
                    var workItem = this.worker.AddWorkItem(wi.Skill, wi.Minutes);
                    workerTask.WorkItems.Add(workItem);
                }
                if (this.worker.CurrentTasks.TryAdd(workerTask.Id, workerTask))
                {
                    newTasks.Add(workerTask);
                }
            }
            return newTasks;
        }

        internal StatementOfWork RenderAsStatementOfWork(WorkerTask workerTask)
        {
            var sow = new StatementOfWork();
            StatementOfWork.Task task = RenderAsStatementOfWorkTask(workerTask);
            sow.Add(task);
            return sow;
        }

        private static StatementOfWork.Task RenderAsStatementOfWorkTask(WorkerTask workerTask)
        {
            return new StatementOfWork.Task
            {
                Description = workerTask.Description,
                DueDateTime = workerTask.DueDateTime,
                RequiredSkills = workerTask.RequiredSkills,
                EstimatedWork = workerTask.WorkItems.Select(wi =>
                        new StatementOfWork.WorkItem() { Minutes = wi.Minutes, Skill = wi.Skill }).ToList(),
                CompletedWork = workerTask.WorkItems.Select(wi =>
                        new StatementOfWork.WorkItem() { Minutes = wi.Completed, Skill = wi.Skill }).ToList(),
                Status = workerTask.GetStatus()
            };
        }

        internal WorkerTask Delete(int id)
        {
            WorkerTask task = null;
            if (this.worker.CurrentTasks.TryRemove(id, out task))
            {
                return task;
            }
            else
            {
                return null;
            }

        }

        internal WorkerTask GetTask(int id)
        {
            WorkerTask task = null; 
            if (this.worker.CurrentTasks.TryGetValue(id, out task) )
            {
                return task;
            } else
            {
                return null;
            }
        }

        internal StatementOfWork GetTasks()
        {
            var sow = new StatementOfWork();

            foreach (var workerTask in this.worker.CurrentTasks.Values)
            {
                var sowTask = RenderAsStatementOfWorkTask(workerTask);
                sow.Add(sowTask);
            }
            return sow;
        }
    }

}
