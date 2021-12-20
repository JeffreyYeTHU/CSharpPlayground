using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ThreadPoolExhaustion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThreadPoolTestController : ControllerBase
    {
        [HttpPost("get_int")]
        public int Get()
        {
            //Thread.Sleep(TimeSpan.FromSeconds(60));
            WaistCpu();
            return Random.Shared.Next(100);
        }

        [HttpPost("get_int/async")]
        public async Task<int> GetAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(60));
            WaistCpu();
            return Random.Shared.Next(100);
        }

        private void WaistCpu()
        {
            var start = DateTime.UtcNow;
            int var = 0;
            while (true)
            {
                if((DateTime.UtcNow - start).TotalMilliseconds > 100)
                    break;
                for (int i = 0; i < 10; i++)
                    var++;
            }
        }
    }
}
