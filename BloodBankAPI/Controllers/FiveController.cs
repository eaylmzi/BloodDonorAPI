using Microsoft.AspNetCore.Mvc;

namespace BloodBankAPI.Controllers
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