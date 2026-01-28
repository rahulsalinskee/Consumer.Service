using Consumer.Shared.DTOs.EmployeeReportDTOs;
using Consumer.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consumer.Shared.Map
{
    public static class EmployeeReportMapper
    {
        public static EmployeeReport ConvertEmployeeReportDtoToEmployeeReport(this EmployeeReportDto employeeReportDto)
        {
            return new EmployeeReport()
            {
                Id = employeeReportDto.Id,
                EmployeeId = employeeReportDto.EmployeeId,
                FirstName = employeeReportDto.FirstName,
                LastName = employeeReportDto.LastName,
            };
        }

        public static EmployeeReportDto ConvertEmployeeReportToEmployeeReportDto(this EmployeeReport employeeReport)
        {
            return new EmployeeReportDto()
            {
                Id = employeeReport.Id,
                EmployeeId = employeeReport.EmployeeId,
                FirstName = employeeReport.FirstName,
                LastName = employeeReport.LastName,
            };
        }
    }
}
