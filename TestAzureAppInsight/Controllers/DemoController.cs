using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestAzureAppInsight.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly string url = "https://testappinsight-fun.azurewebsites.net/api/HttpTrigger1?code=4AHApebO4dkP6V8mm7x_iNEVbexRMpDwb0ZpFqrLzylkAzFumFhsZQ==";
        private ILogger<DemoController> logger;

        public DemoController(ILogger<DemoController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            logger.LogInformation("call func to get data");
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            var text = await response.Content.ReadAsStringAsync();
            logger.LogInformation("data receive success");

            return new OkObjectResult(text);
            //return new OkObjectResult("hello jeff");
        }
    }
}
