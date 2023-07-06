using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Deparamtes
{
    public class AppDbContext : DbContext
    {
        public DbSet<DepartmentModel> Departamentos { get; set; } = null!;

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
