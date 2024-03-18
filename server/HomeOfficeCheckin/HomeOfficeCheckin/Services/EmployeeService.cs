using HomeOfficeCheckin.Data;
using HomeOfficeCheckin.Models;
using HomeOfficeCheckin.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace HomeOfficeCheckin.Services
{
    /// <summary>
    /// Service class responsible for managing employee data.
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        private readonly HomeOfficeTimeDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeService"/> class.
        /// </summary>
        /// <param name="context">The database context for accessing employee data.</param>
        public EmployeeService(HomeOfficeTimeDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all employees asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, returning a read-only list of employees.</returns>
        public async Task<IReadOnlyList<Employee>> GetAllEployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        /// <summary>
        /// Retrieves an employee by their ID asynchronously.
        /// </summary>
        /// <param name="Id">The ID of the employee to retrieve.</param>
        /// <returns>A task representing the asynchronous operation, returning the employee.</returns>
        public async Task<Employee> GetEployeeByIdAsync(string Id)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Id == Id);
        }

        /// <summary>
        /// Retrieves an employee by their username asynchronously.
        /// </summary>
        /// <param name="username">The username of the employee to retrieve.</param>
        /// <returns>A task representing the asynchronous operation, returning the employee.</returns>
        public async Task<Employee> GetEployeeByUserNameAsync(string username)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.UserName == username);
        }
    }
}
