using Consumer.Api.Repositories.Implementations;
using Consumer.Api.Repositories.Services;
using Consumer.DataBase;
using Consumer.Log;
using Microsoft.EntityFrameworkCore;
using Serilog;

/* 1. Setup Serilog first */
Log.Logger = LogConfiguration.GenerateConsumerLog();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

/* Register Db Context */
builder.Services.AddDbContext<EmployeeReportDbContext>(options => options.UseSqlServer(connectionString: "ConnectionStringForDb_EmployeeApplication"));

builder.Services.AddScoped<IReportService, ReportServiceImplementation>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(option => option.SwaggerEndpoint(url: "/openapi/v1.json", name: "Consumer API"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
