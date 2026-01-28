using Confluent.Kafka;
using Consumer.Exception;
using Consumer.Exception.ExceptionRepository.ExceptionServices;
using Consumer.Repositories.Repositories.Services;
using Consumer.Shared.DTOs.EmployeeDTOs;
using System.Text.Json;

namespace Consumer.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly ConsumerConfig _consumerConfig;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, GlobalException globalException)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

            _consumerConfig = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                ClientId = "consumer-client",
                GroupId = "employee-consumer-group",
                /* This means earliest message will be consumed/read. 
                *  Just like : If Someone joins any existing WhastApp group and cannot see the previous messages sent by the first person
                *  This line will make sure that the person who joins the group in the last will also see the previous messages
                */
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false // We will commit manually after success
            };
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker started...");

            using var consumer = new ConsumerBuilder<string, string>(_consumerConfig).Build();

            /* Subscribe to multiple topics */
            consumer.Subscribe(new[] { "add-employee-topic", "update-employee-topic", "delete-employee-topic" });

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var result = consumer.Consume(TimeSpan.FromSeconds(3));

                        if (result != null)
                        {
                            // Create a SCOPE for this specific message processing
                            using (var scope = _serviceProvider.CreateScope())
                            {
                                // Get services from the fresh scope
                                var service = scope.ServiceProvider.GetRequiredService<IEmployeeService>();
                                var exceptionHelper = scope.ServiceProvider.GetRequiredService<IExceptionService>();

                                await exceptionHelper.ExecuteSafeAsync(async () =>
                                {
                                    var messageValue = result.Message.Value;
                                    var topic = result.Topic;

                                    _logger.LogInformation("Received message on {Topic}", topic);

                                    var employeeDto = JsonSerializer.Deserialize<EmployeeDto>(messageValue);

                                    if (employeeDto != null)
                                    {
                                        string action = topic.Contains("add") ? "ADD" : "UPDATE";
                                        await service.ProcessEmployeeAsync(employeeDto, action);

                                        // Commit offset only on success
                                        consumer.Commit(result);
                                    }
                                });
                            }
                        }
                    }
                    catch (ConsumeException e)
                    {
                        _logger.LogError("Kafka error: {Reason}", e.Error.Reason);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Worker stopping...");
                consumer.Close();
            }
        }
    }
}

