using Consumer.Exception.ExceptionRepository.ExceptionServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consumer.Exception.ExceptionRepository.ExceptionImplementations
{
    public class ExceptionImplementation : IExceptionService
    {
        private readonly ILogger<ExceptionImplementation> _logger;

        public ExceptionImplementation(ILogger<ExceptionImplementation> logger)
        {
            _logger = logger;
        }

        public async Task ExecuteSafeAsync(Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (System.Exception ex)
            {
                var errorId = Guid.NewGuid();
                _logger.LogError(ex, "Application Error ID: {ErrorId} | Message: {Message}", errorId, ex.Message);
            }
        }
    }
}
