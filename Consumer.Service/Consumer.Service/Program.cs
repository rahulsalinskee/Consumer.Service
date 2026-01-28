using Consumer.DataBase;
using Consumer.Exception.ExceptionRepository.ExceptionImplementations;
using Consumer.Exception.ExceptionRepository.ExceptionServices;
using Consumer.Log;
using Consumer.Repositories.Repositories.Implementations;
using Consumer.Repositories.Repositories.Services;
using Consumer.Service; // Namespace for Worker
using Microsoft.EntityFrameworkCore;
using Serilog;

/* 1. Setup Serilog first */
Log.Logger = LogConfiguration.GenerateConsumerLog();

try
{
    var host = Host.CreateDefaultBuilder(args).UseSerilog().ConfigureServices((hostContext, services) =>
        {
            var configuration = hostContext.Configuration;

            /*  2. Add DbContext */
            services.AddDbContext<EmployeeReportDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ConnectionStringForDb_EmployeeApplication")));

            /*  3. Register Repositories & Services (Scoped) */
            services.AddScoped<IEmployeeReporterService, EmployeeReporterServiceImplementation>();
            services.AddScoped<IEmployeeService, EmployeeServiceImplementation>();

            /*  4. Register Exception Helper (Singleton or Scoped are both fine, Scoped is safer) */
            services.AddScoped<IExceptionService, ExceptionImplementation>();

            /*  5. Register the GlobalException class (Add this line!)
                We use AddSingleton because Worker is a Singleton and GlobalException is stateless */
            services.AddSingleton<Consumer.Exception.GlobalException>();

            /*  6. Register the Worker (Hosted Service) */
            services.AddHostedService<Worker>();
        })
        .Build();

    await host.RunAsync();
}
catch (System.Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}