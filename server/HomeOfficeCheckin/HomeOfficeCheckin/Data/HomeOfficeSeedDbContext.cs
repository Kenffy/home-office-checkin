using HomeOfficeCheckin.Data;
using HomeOfficeCheckin.Models;
using System.Text.Json;

namespace HomeOfficeChecking.Data
{
    /// <summary>
    /// Class responsible for seeding initial data into the home office time database context.
    /// </summary>
    public class HomeOfficeSeedDbContext
    {
        /// <summary>
        /// Seeds initial data into the database asynchronously.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="loggerFactory">The logger factory for logging any errors that occur during seeding.</param>
        public static async Task SeedDataAsync(HomeOfficeTimeDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                // Seed Employees
                if (!context.Employees.Any())
                {
                    var employeeData = File.ReadAllText("./Data/SeedData/employees.json");
                    var employees = JsonSerializer.Deserialize<List<Employee>>(employeeData);

                    foreach (var employee in employees)
                    {
                        employee.Id = Guid.NewGuid().ToString();
                        context.Employees.Add(employee);
                    }

                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<HomeOfficeSeedDbContext>();
                logger.LogError(ex.Message);
            }
        }
    }
}
