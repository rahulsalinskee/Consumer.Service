using Consumer.Api.Repositories.Services;
using Consumer.DataBase;
using Consumer.Shared.DTOs.ResponseDTOs;
using Consumer.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Consumer.Api.Repositories.Implementations
{
    public class ReportServiceImplementation : IReportService
    {
        private readonly EmployeeReportDbContext _employeeReportDbContext;
        private readonly ILogger<ReportServiceImplementation> _logger;

        public ReportServiceImplementation(EmployeeReportDbContext employeeReportDbContext, ILogger<ReportServiceImplementation> logger)
        {
            this._employeeReportDbContext = employeeReportDbContext;
            this._logger = logger;
        }

        public async Task<ResponseDto> GetAllReportsAsync()
        {
            var response = this._employeeReportDbContext.EmployeeReports.Select(report => new Employee()).ToListAsync();

            if (response is null)
            {
                return new ResponseDto()
                {
                    Result = null,
                    IsSuccess = false,
                    Message = "Could not get reports"
                };
            }

            return new ResponseDto()
            {
                Result = response,
                IsSuccess = true,
                Message = "Success"
            };
        }

        public async Task<ResponseDto> GetReportByIdAsync(Guid id)
        {
            var response = await this._employeeReportDbContext.EmployeeReports.FirstOrDefaultAsync(report => report.Id == id);

            if (response is null)
            {
                ApplicationError applicationError = new ()
                {
                    ID = Guid.NewGuid(),
                    ErrorMessage = "Could not get report",
                    DateTimeOfError = DateTime.Now,
                    
                };

                return new ResponseDto()
                {
                    Result = null,
                    IsSuccess = false,
                    Message = applicationError.ErrorMessage,
                };
            }

            return new ResponseDto()
            {
                Result = response,
                IsSuccess = true,
                Message = "Success",
            };
        }
    }
}