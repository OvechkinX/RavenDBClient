using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RavenRBD.Model;

namespace RavenRBD.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RavenController : ControllerBase
    {
        [HttpGet("1")]
        public ActionResult<IEnumerable<Order>> Get()
        {
            Raven raven = new Raven();
            var result = raven.Query1();
            return new JsonResult(result);

        }

        [HttpGet("2")]
        public ActionResult<IEnumerable<Order>> Get2()
        {
            Raven raven = new Raven();
            var result = raven.Query2();
            return new JsonResult(result);

        }

        [HttpGet("3")]
        public ActionResult<IEnumerable<Order>> Get3()
        {
            Raven raven = new Raven();
            var result = raven.Query3();
            return new JsonResult(result);

        }
    }
}