using Confluent.Kafka;
using Consumer.Shared;
using System.Text.Json;

namespace Consumer.Service
{
    public class Worker(ILogger<Worker> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                ConsumerConfig consumerConfiguration = new()
                {
                    BootstrapServers = "localhost:9092",
                    ClientId = "consumer-client",
                    GroupId = "employee-consumer-group"
                };

                using (IConsumer<string, string> consumer = new ConsumerBuilder<string, string>(consumerConfiguration).Build())
                {
                    consumer.Subscribe(topic: "add-employee-topic");
                    consumer.Subscribe(topic: "update-employee-topic");
                    consumer.Subscribe(topic: "delete-employee-topic");

                    /* Get the data from Apache Kafka in every 3 seconds */
                    var consumerData = consumer.Consume(TimeSpan.FromSeconds(3));

                    if (consumerData is not null)
                    {
                        /* Insert the data into the database */
                        var employee = JsonSerializer.Deserialize<Employee>(consumerData.Message.Value);
                    }
                };
                if (logger.IsEnabled(LogLevel.Information))
                {
                    logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
