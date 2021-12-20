using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace ElkTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetCountersController : ControllerBase
    {
        [HttpPost("log_net_counters")]
        public async Task LogNetCounter()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}
