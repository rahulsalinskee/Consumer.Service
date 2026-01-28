using System;
using System.Collections.Generic;
using System.Text;

namespace Consumer.Shared.DTOs.EmployeeReportDTOs
{
    public class EmployeeReportDto
    {
        public Guid Id { get; set; }

        public Guid EmployeeId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
    }
}
