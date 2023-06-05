using Microsoft.AspNetCore.Mvc;

namespace DonorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FourController : ControllerBase
    {
        [HttpGet]
        public ActionResult<int> four()
        {
            return Ok(4);
        }
    }
}