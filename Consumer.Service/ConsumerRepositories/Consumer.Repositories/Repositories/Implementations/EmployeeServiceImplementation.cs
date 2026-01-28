using Consumer.Repositories.Repositories.Services;
using Consumer.Shared.DTOs.EmployeeDTOs;
using Consumer.Shared.DTOs.EmployeeReportDTOs;
using Microsoft.Extensions.Logging;

namespace Consumer.Repositories.Repositories.Implementations
{
    public class EmployeeServiceImplementation : IEmployeeService
    {
        private readonly IEmployeeReporterService _employeeReporterService;
        private readonly ILogger<EmployeeServiceImplementation> _logger;

        public EmployeeServiceImplementation(IEmployeeReporterService employeeReporterService, ILogger<EmployeeServiceImplementation> logger)
        {
            this._employeeReporterService = employeeReporterService;
            this._logger = logger;
        }

        public async Task ProcessEmployeeAsync(EmployeeDto employeeDto, string actionType)
        {
            _logger.LogInformation("Processing {Action} for Employee {Id}", actionType, employeeDto.Id);

            AddEmployeeReportDto addEmployeeReportDto = new()
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName
            };

            UpdateEmployeeReportDto updateEmployeeReportDto = new()
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName
            };

            if (actionType == "ADD")
            {
                await _employeeReporterService.AddEmployeeReportAsync(addEmployeeReportDto: addEmployeeReportDto);
            }
            else if (actionType == "UPDATE")
            {
                await _employeeReporterService.UpdateEmployeeReportAsync(id: employeeDto.Id, updateEmployeeReportDto: updateEmployeeReportDto);
            }

            _logger.LogInformation("Successfully processed {Id}", employeeDto.Id);
        }
    }
}
