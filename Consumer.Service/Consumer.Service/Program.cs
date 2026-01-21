using Consumer.DataBase;
using Consumer.Service;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<EmployeeReportDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString(name: "ConnectionStringForDb_EmployeeApplication"));
});

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
