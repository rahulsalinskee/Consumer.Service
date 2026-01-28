using System;
using System.Collections.Generic;
using System.Text;

namespace Consumer.Shared.DTOs.ResponseDTOs
{
    public class ResponseDto
    {
        public object? Result { get; set; } = null;

        public string Message { get; set; } = string.Empty;

        public bool IsSuccess { get; set; } = false;
    }
}
