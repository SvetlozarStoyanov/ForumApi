using Microsoft.AspNetCore.Mvc;

namespace ForumApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Hello()
        {
            return Ok("Hello from forum Api!");
        }
    }
}
