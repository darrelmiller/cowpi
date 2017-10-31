using cowpi.Representations;
using cowpi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cowpi.Controllers
{
    [Route("api/capacity")]
    public class CapacityController
    {
        private CapacityService capacityService;

        public CapacityController(CapacityService capacityService)
        {
            this.capacityService = capacityService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            StatementOfWork sow = this.capacityService.GetCapacity();

            return new JsonResult(sow, StatementOfWork.SerializerSettings);
        }
    }
}
