using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using doWorkLib;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SharedController : ControllerBase
    {
        private readonly ILogger<SharedController> _logger;

        public SharedController(ILogger<SharedController> logger)
        {
            _logger = logger;
        }

        [HttpGet("add/{a}/{b}")]
        public int Add(int a, int b)
        {
            return SharedClass.add(a, b);
        }

        [HttpGet("sub/{a}/{b}")]
        public int Sub(int a, int b)
        {
            return SharedClass.sub(a, b);
        }

        [HttpGet("mul/{a}/{b}")]
        public int Mul(int a, int b)
        {
            return SharedClass.mul(a, b);
        }

        [HttpGet("div/{a}/{b}")]
        public int Div(int a, int b)
        {
            return SharedClass.div(a, b);
        }
    }
}