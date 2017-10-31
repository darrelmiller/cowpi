using cowpi.Representations;
using cowpi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cowpi.Controllers
{
    [Route("")]
    public class HomeController
    {
        private Worker worker;

        public HomeController(Worker worker)
        {
            this.worker = worker;
        }

        [HttpGet()]
        public IActionResult Get()
        {
            var cow = this.worker.CooperatingWorker;
            return new JsonResult(cow, StatementOfWork.SerializerSettings);
        }

    }
}
