using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MysqlPerTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataWriteController : ControllerBase
    {
        [HttpPost("write_data")]
        public async Task WriteData()
        {

        }
    }
}
