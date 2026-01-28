using System;
using System.Collections.Generic;
using System.Text;

namespace Consumer.Shared.DTOs.EmployeeDTOs
{
    public class UpdateEmployeeDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
    }
}
