using Consumer.Shared.Models;
using Microsoft.Extensions.Logging;
namespace Consumer.Exception
{
    public class GlobalException
    {
        private readonly ILogger<GlobalException> _logger;

        public GlobalException(ILogger<GlobalException> logger)
        {
            _logger = logger;
        }

        // Pass the Action/Task you want to run safely
        public async Task ExecuteSafeAsync(Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (System.Exception exception)
            {
                // Logic to handle the error purely internally
                var errorId = Guid.NewGuid();

                _logger.LogError(exception, "Application Error ID: {ErrorId}", errorId);
                _logger.LogError("Error Message: {Message}", exception.Message);

                // Note: In a worker, you cannot return "Status 500". 
                // You usually Log it, Ack/Nack a queue message, or stop the service.
            }
        }
    }
}