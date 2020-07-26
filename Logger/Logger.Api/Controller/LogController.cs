using Logger.Common;
using Logger.Data.EF;
using Logger.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Logger.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        [Route("")]
        [HttpPost]
        public ActionResult AddLog(AddLogRequest request)   
        {
            _logService.AddLog(request.message, request.obj);
            return NoContent();
        }

        [Route("")]
        [HttpGet]
        public Log[] GetLogs()
        {
            return _logService.GetLogs();
        }
    }
}