using cowpi.Representations;
using cowpi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace cowpi.Controllers
{
    [Route("peers")]
    public class PeersController : Controller
    {
        private PeerService peerService;

        public PeersController(PeerService peerService)
        {
            this.peerService = peerService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            CooperatingWorkers cows = this.peerService.GetPeers();

            return new JsonResult(new { cooperatingWorkers = cows }, StatementOfWork.SerializerSettings);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var peer = this.peerService.GetPeer(id);

            if (peer == null)
            {
                return new NotFoundResult();
            }

            var cow = new CooperatingWorker()
            {
                Skills = peer.Skills,
                TasksUrl = peer.TasksUrl,
                CapacityUrl = peer.CapacityUrl
            };
            return new JsonResult(new { cooperatingWorkers = cow }, StatementOfWork.SerializerSettings);
        }

        [HttpPost]
        public IActionResult Post()
        {
            CooperatingWorker cow;
            using (var sr = new StreamReader(this.Request.Body))
            {
                try
                {
                    cow = CooperatingWorker.Load(sr);
                }
                catch
                {
                    return new BadRequestResult();
                }
            }

            var peer = this.peerService.CreatePeer(cow);
            return new CreatedAtActionResult("get", "peers", new { id = peer.Id }, null);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var peer = this.peerService.Delete(id);

            if (peer == null)
            {
                return new NotFoundResult();
            }
            else
            {
                return new NoContentResult();
            }

        }

    }
}
