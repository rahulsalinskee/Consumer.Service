using System;
using System.Collections.Generic;
using System.Text;

namespace Consumer.Shared.Models
{
    public class ApplicationError
    {
        public Guid ID { get; set; } = Guid.NewGuid();

        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;

        public DateTime DateTimeOfError { get; set; }
    }
}
