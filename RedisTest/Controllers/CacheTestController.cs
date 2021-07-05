using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using RedisTest.ViewModel;
using RedisTest.Extensions;

namespace RedisTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CacheTestController : ControllerBase
    {
        private IDistributedCache _cache;
        private readonly ILogger<CacheTestController> _logger;

        public CacheTestController(ILogger<CacheTestController> logger, IDistributedCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        [HttpPost("save_to_cache")]
        public async Task<ActionResult<bool>> SaveToCache(Persion persion)
        {
            await _cache.SetRecordAsync(persion.Id, persion);
            return true;
        }

        [HttpPost("get_from_cache")]
        public async Task<ActionResult<Persion>> GetFromCache(string id)
        {
            Persion p = await _cache.GetRecordAsync<Persion>(id);
            return p;
        }
    }
}
