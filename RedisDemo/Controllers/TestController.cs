using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

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

        /// <summary>
        /// Using the string type of redis
        /// ref: https://stackexchange.github.io/StackExchange.Redis/Basics
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost("test_string_type")]
        public async Task<string> EchoString(string key, string value)
        {
            var db = _redis.GetDatabase();
            await db.StringSetAsync(key, value);
            string echo = await db.StringGetAsync(key);
            return echo;
        }

        ///// <summary>
        ///// Test the list type of redis
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="values"></param>
        ///// <returns></returns>
        //[HttpPost("test_list_type")]
        //public async Task<string> EchoList(string key, IList<int> values)
        //{
        //    var db = _redis.GetDatabase();
        //}
    }
}
