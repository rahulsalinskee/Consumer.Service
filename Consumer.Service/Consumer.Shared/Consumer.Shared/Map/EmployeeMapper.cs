using Consumer.Shared.DTOs.EmployeeDTOs;
using Consumer.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consumer.Shared.Map
{
    public static class EmployeeMapper
    {
        public static Employee ConvertEmployeeDtoToEmployee(this EmployeeDto employeeDto)
        {
            return new Employee()
            {
                Id = employeeDto.Id,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName
            };
        }

        public static EmployeeDto ConvertEmployeeToEmployeeDto(this Employee employee)
        {
            return new EmployeeDto()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName
            };
        }
    }
}
