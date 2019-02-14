using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;
using TTY.Api.Throttle;

namespace TTY.GMP.WebApi.Controllers
{
    /// <summary>
    /// 默认请求
    /// </summary>
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "hello gmp";
        }
    }
}
