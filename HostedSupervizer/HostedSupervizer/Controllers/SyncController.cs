using Domain.Entities;
using Domain.Models;
using HostedSupervizer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HostedSupervizer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SyncController : ControllerBase
    {
        private readonly Supervizer _supervizer;

        public SyncController(Supervizer supervizer)
        {
            _supervizer = supervizer;
        }

        [HttpPost]
        public IActionResult Synk(SyncEntity entity)
        {
            Console.WriteLine("O venit");
            var result = _supervizer.Synk(entity);
            Console.WriteLine("StorageQueue updatata cu succes -> " + result);
            return Ok(new { Result = result });
        }
    }
}
