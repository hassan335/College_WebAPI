using CollegeApp.MyLogging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        //private readonly IMyLogger _Logger;

        private readonly ILogger<DemoController> _logger; 




        public DemoController(ILogger<DemoController> logger)
        {
            //_Logger = new LogToDB();
            _logger = logger;
        }
        [HttpGet]
        public ActionResult Index()
        {
            //_Logger.log("Index Method Started");
            _logger.LogTrace("Logging from trace");
            _logger.LogDebug("Logging from debug");
            _logger.LogInformation("Logging from info");
            _logger.LogWarning("Logging from warning");
            _logger.LogError("Logging from Error");
            _logger.LogCritical("Logging from Critical");

            return Ok();
        }
    }
}
