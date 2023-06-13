using Microsoft.AspNetCore.Mvc;

namespace BloodBankAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BloodRequestController : Controller
    {
        public ActionResult<int> RequestBlood()
        {
            
            return View();
        }
    }
}
