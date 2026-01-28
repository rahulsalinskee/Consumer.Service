using Consumer.Shared.DTOs.EmployeeDTOs;

namespace Consumer.Repositories.Repositories.Services
{
    public interface IEmployeeService
    {
        public Task ProcessEmployeeAsync(EmployeeDto employeeDto, string actionType);
    }
}
