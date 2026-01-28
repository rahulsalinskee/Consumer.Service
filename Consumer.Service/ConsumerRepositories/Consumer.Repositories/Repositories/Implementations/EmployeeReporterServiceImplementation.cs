using Consumer.DataBase;
using Consumer.Repositories.Repositories.Services;
using Consumer.Shared.DTOs.EmployeeReportDTOs;
using Consumer.Shared.DTOs.ResponseDTOs;
using Consumer.Shared.Map;
using Consumer.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Consumer.Repositories.Repositories.Implementations
{
    public class EmployeeReporterServiceImplementation : IEmployeeReporterService
    {
        private readonly EmployeeReportDbContext _employeeReportDbContext;

        public EmployeeReporterServiceImplementation(EmployeeReportDbContext employeeReportDbContext)
        {
            _employeeReportDbContext = employeeReportDbContext;
        }

        public async Task<ResponseDto> AddEmployeeReportAsync(AddEmployeeReportDto addEmployeeReportDto)
        {
            await this._employeeReportDbContext.

            if (addEmployeeReportDto is null)
            {
                ApplicationError applicationError = new()
                {
                    ID = Guid.NewGuid(),
                    ErrorMessage = "Employee report is null",
                    DateTimeOfError = DateTime.Now,
                };

                return new ResponseDto()
                {
                    Result = null,
                    IsSuccess = false,
                    Message = applicationError.ErrorMessage
                };
            }

            var isEmployeeReportExist = await IsEmployeeReportExistAsync(firstName: addEmployeeReportDto.FirstName, lastName: addEmployeeReportDto.LastName);

            if (isEmployeeReportExist)
            {
                ApplicationError applicationError = new()
                {
                    ID = Guid.NewGuid(),
                    ErrorMessage = "Employee report already exists with same first name and last name",
                    DateTimeOfError = DateTime.Now,
                };

                return new ResponseDto()
                {
                    Result = null,
                    IsSuccess = false,
                    Message = applicationError.ErrorMessage,
                };
            }

            EmployeeReportDto employeeReportDto = new()
            {
                FirstName = addEmployeeReportDto.LastName,
                LastName = addEmployeeReportDto.LastName,
            };

            var addEmployeeReport = employeeReportDto.ConvertEmployeeReportDtoToEmployeeReport();

            await _employeeReportDbContext.EmployeeReports.AddAsync(entity: addEmployeeReport);
            await _employeeReportDbContext.SaveChangesAsync();

            var addedEmployeeReportDto = addEmployeeReport.ConvertEmployeeReportToEmployeeReportDto();

            return new ResponseDto()
            {
                Result = addedEmployeeReportDto,
                IsSuccess = true,
                Message = "Employee report added successfully"
            };
        }

        public async Task<ResponseDto> UpdateEmployeeReportAsync(Guid id, UpdateEmployeeReportDto updateEmployeeReportDto)
        {
            if (id == Guid.Empty)
            {
                ApplicationError applicationError = new()
                {
                    ID= Guid.NewGuid(),
                    ErrorMessage = "Id is empty",
                    DateTimeOfError = DateTime.Now,
                };

                return new ResponseDto()
                {
                    Result = null,
                    IsSuccess = false,
                    Message = applicationError.ErrorMessage
                };
            }

            if (updateEmployeeReportDto is null)
            {
                ApplicationError applicationError = new()
                {
                    ID = Guid.NewGuid(),
                    ErrorMessage = "Employee report is null",
                    DateTimeOfError = DateTime.Now,
                };

                return new ResponseDto()
                {
                    Result = null,
                    IsSuccess = false,
                    Message = applicationError.ErrorMessage
                };
            }

            var isEmployeeReportExistAsync = await IsEmployeeReportExistAsync(firstName: updateEmployeeReportDto.FirstName, lastName: updateEmployeeReportDto.LastName);

            if (isEmployeeReportExistAsync)
            {
                ApplicationError applicationError = new()
                {
                    ID = Guid.NewGuid(),
                    ErrorMessage = "Employee report already exists with same first name and last name",
                    DateTimeOfError = DateTime.Now,
                };

                return new ResponseDto()
                {
                    Result = null,
                    IsSuccess = false,
                    Message = applicationError.ErrorMessage,
                };
            }

            EmployeeReport employeeReport = new()
            {
                FirstName = updateEmployeeReportDto.FirstName,
                LastName = updateEmployeeReportDto.LastName,
            };

            // Simple update logic example
            _employeeReportDbContext.EmployeeReports.Update(entity: employeeReport);
            await _employeeReportDbContext.SaveChangesAsync();

            var updatedEmployeeReportDto = employeeReport.ConvertEmployeeReportToEmployeeReportDto();

            return new ResponseDto()
            {
                Result = updatedEmployeeReportDto,
                IsSuccess = true,
                Message = "Employee report updated successfully"
            };
        }

        private async Task<bool> IsEmployeeReportExistAsync(string firstName, string lastName)
        {
            return await _employeeReportDbContext.EmployeeReports.AnyAsync(employeeReport => employeeReport.FirstName == firstName && employeeReport.LastName == lastName);
        }
    }
}
