using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cowpi.Representations;
using System.IO;
using cowpi.Services;

namespace Cowpi.Controllers
{
    [Route("api/[controller]")]
    public class TasksController : Controller
    {

        private TaskService taskService;

        public TasksController(TaskService taskService)
        {
            this.taskService = taskService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            StatementOfWork sow = this.taskService.GetTasks();

            return new JsonResult(sow, StatementOfWork.SerializerSettings);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var task = this.taskService.GetTask(id);

            if (task == null)
            {
                return new NotFoundResult();
            }

            var sow = this.taskService.RenderAsStatementOfWork(task) ;
            return new JsonResult(sow, StatementOfWork.SerializerSettings);
        }

        [HttpPost]
        public IActionResult Post()
        {
            StatementOfWork sow;
            using (var sr = new StreamReader(this.Request.Body)) {
                try
                {
                    sow = StatementOfWork.Load(sr);
                } catch
                {
                    return new BadRequestResult();
                }
            }

            var tasks = this.taskService.CreateTasks(sow);
            return new CreatedAtActionResult("get", "tasks", new { id = tasks.First().Id },null);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var task = this.taskService.Delete(id);

            if (task == null)
            {
                return new NotFoundResult();
            } else
            {
                return new NoContentResult();
            }

        }
    }
}
