using Deparamtes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Project1.Models;

namespace Project1
{
    public class AppDbContext : DbContext
    {
        public DbSet<DepartmentModel> Departamentos { get; set; } = null!;

        public DbSet<EmployeeModel> Employess { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json")
           .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        }

    }
}
