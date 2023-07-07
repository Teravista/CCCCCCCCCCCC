using Microsoft.EntityFrameworkCore;
using Project3.Models;

namespace Project3
{
    public class AppDbContext : DbContext
    {
        public DbSet<ContactModel> Contacts { get; set; } = null!;

        public DbSet<CategoryModel> Categories { get; set; } = null!;
        public DbSet<SubCategoryModel> SubCategories { get; set; } = null!;
        public DbSet<LoginModel> Logins { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json")
           .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        }
        protected override void OnModelCreating(ModelBuilder builder)
        { 
            //Method to ensure that Emails of Contacts will be unique
            builder.Entity<ContactModel>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }

    }
}
