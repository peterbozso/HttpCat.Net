using Microsoft.AspNetCore.Mvc;

namespace HttpCat.Api.Controllers
{
    [ApiController]
    public class StatusCodeController : ControllerBase
    {
        [HttpGet]
        [Route("{statusCode:int:required}")]
        public StatusCodeResult Get(int statusCode)
        {
            return StatusCode(statusCode);
        }
    }
}