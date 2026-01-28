using Consumer.Shared.DTOs.EmployeeReportDTOs;
using Consumer.Shared.DTOs.ResponseDTOs;

namespace Consumer.Repositories.Repositories.Services
{
    public interface IEmployeeReporterService
    {
        public Task<ResponseDto> AddEmployeeReportAsync(AddEmployeeReportDto addEmployeeReportDto);

        public Task<ResponseDto> UpdateEmployeeReportAsync(Guid id, UpdateEmployeeReportDto updateEmployeeReportDto);
    }
}
