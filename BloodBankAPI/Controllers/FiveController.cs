using Microsoft.AspNetCore.Mvc;

namespace bloodbank.Logic.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FiveController : ControllerBase
    {
        [HttpGet]
        public ActionResult<int> five()
        {
            return Ok(5);
        }
    }
}