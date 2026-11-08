using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,SuperAdmin", AuthenticationSchemes = "LoginForMicrosoft")]
    public class MicrosoftController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok ("This a Microsoft Controller");
        }








    }
}
