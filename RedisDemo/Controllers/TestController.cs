using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RedisDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IConnectionMultiplexer _redis;

        public TestController(IConnectionMultiplexer redis)
        {
            _redis = redis ?? throw new ArgumentNullException(nameof(redis));
        }

        /// <summary>
        /// Redis ping round trip time
        /// ref: https://docs.redis.com/latest/rs/references/client_references/client_csharp/
        /// </summary>
        /// <returns></returns>
        [HttpPost("ping_redis_rtt")]
        public async Task<string> PingRedisRtt()
        {
            var db = _redis.GetDatabase();
            var pong = await db.PingAsync();
            return pong.ToString();
        }
    }
}
