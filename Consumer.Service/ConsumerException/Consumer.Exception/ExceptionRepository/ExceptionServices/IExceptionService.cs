using System;
using System.Collections.Generic;
using System.Text;

namespace Consumer.Exception.ExceptionRepository.ExceptionServices
{
    public interface IExceptionService
    {
        public Task ExecuteSafeAsync(Func<Task> action);
    }
}
