using Consumer.Api.Repositories.Services;
using Microsoft.AspNetCore.Mvc;

namespace Consumer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeReportController : ControllerBase
    {
        private readonly ILogger<EmployeeReportController> _logger;
        private readonly IReportService _reportService;

        public EmployeeReportController(ILogger<EmployeeReportController> logger, IReportService reportService)
        {
            this._logger = logger;
            this._reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeReports()
        {
            var response = await this._reportService.GetAllReportsAsync();

            if (response.IsSuccess)
            {
                _logger.LogInformation(message: "Fetched all consumer data successfully");
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
