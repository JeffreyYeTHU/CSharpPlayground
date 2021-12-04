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

        /// <summary>
        /// Test the list type of redis
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        [HttpPost("test_list_type")]
        public async Task<List<int>> EchoList(string key, IList<int> values)
        {
           var db = _redis.GetDatabase();

           // add values to the list
           // these values will be pushed one by one
           await db.ListRightPushAsync(key, values.Select(v => (RedisValue)v).ToArray());
           await db.ListLeftPushAsync(key, values.Select(v => (RedisValue)v).ToArray());

           // get data from the list
           var single = await db.ListGetByIndexAsync(key, 0);
           var list = await db.ListRangeAsync(key);
           return list.Select(v => (int)v).ToList();
        }

        [HttpPost("test_hash_type")]
        public async Task<Dictionary<string, string>> EchoHash(string key, Dictionary<string, string> values)
        {
            var db = _redis.GetDatabase();
            foreach (var kv in values)
            {
                await db.HashSetAsync(key, kv.Key, kv.Value);
            }
            var hash = await db.HashGetAllAsync(key);
            return hash.ToDictionary(k => (string)k.Name, v => (string)v.Value);
        }

        [HttpPost("test_set_type")]
        public async Task<List<string>> EchoSet(string key, IList<string> values)
        {
            var db = _redis.GetDatabase();
            await db.SetAddAsync(key, values.Select(v => (RedisValue)v).ToArray());
            var set = await db.SetMembersAsync(key);
            return set.Select(v => (string)v).ToList();
        }

        [HttpPost("test_sorted_set_type")]
        public async Task<List<string>> EchoSortedSet(string key, IList<string> values)
        {
            var db = _redis.GetDatabase();
            foreach (var value in values)
                await db.SortedSetAddAsync(key, value, value.Length);
            var set = await db.SortedSetRangeByRankAsync(key);
            return set.Select(v => (string)v).ToList();
        }
    }
}
