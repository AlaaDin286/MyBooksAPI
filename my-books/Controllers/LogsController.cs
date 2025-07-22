using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly LogsService logsService;

        public LogsController(LogsService logsService)
        {
            this.logsService = logsService;
        }

        [HttpGet("get-all-logs-from-db")]
        public IActionResult GetAllLogsFromDb()
        {
            try
            {
                var logs = logsService.GetAllLogsFromDb();
                return Ok(logs);
            }
            catch (Exception)
            {
                return BadRequest("Could not load logs from the database.");
            }
        }
    }
}
