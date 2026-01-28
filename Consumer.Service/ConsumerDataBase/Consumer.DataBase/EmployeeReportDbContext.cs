using Consumer.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Consumer.DataBase
{
    public class EmployeeReportDbContext : DbContext
    {
        public EmployeeReportDbContext(DbContextOptions<EmployeeReportDbContext> options) : base(options)
        {
            
        }

        public DbSet<EmployeeReport> EmployeeReports { get; set; }
    }
}
