using HomeOfficeCheckin.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeOfficeCheckin.Data
{
    /// <summary>
    /// Represents the database context for managing home office time entries and employees.
    /// </summary>
    public class HomeOfficeTimeDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeOfficeTimeDbContext"/> class.
        /// </summary>
        /// <param name="options">The options for configuring the context.</param>
        public HomeOfficeTimeDbContext(DbContextOptions<HomeOfficeTimeDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// Gets or sets the DbSet for managing home office time entries.
        /// </summary>
        public DbSet<HomeOfficeTime> HomeOfficeTimes { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for managing employees.
        /// </summary>
        public DbSet<Employee> Employees { get; set; }


        /// <summary>
        /// Configures the entity models and their relationships in the database.
        /// </summary>
        /// <param name="modelBuilder">The model builder instance.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional configuration for entity models can be added here if needed.
        }
    }
}
