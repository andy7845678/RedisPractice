using Microsoft.AspNetCore.Mvc;
using RedisPractice.Models;
using StackExchange.Redis;

namespace RedisPractice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RedisController : ControllerBase
    {
        private readonly IConnectionMultiplexer _redisService;

        public RedisController(IConnectionMultiplexer redisService)
        {
            _redisService = redisService;
        }

        [HttpGet]
        public ActionResult<string> GetValue(string key)
        {
            var value = _redisService.GetDatabase().StringGet(key).ToString();
            return value;
        }

        [HttpPost]
        public ActionResult<HttpResponse> SetValue(RedisModel redisModel)
        {
            //_redisService.GetDatabase().StringSet(redisModel.Key, redisModel.Value);
            //設置expire
            _redisService.GetDatabase().StringSet(redisModel.Key, redisModel.Value, TimeSpan.FromSeconds(60));
            return StatusCode(200);

        }
    }
}
