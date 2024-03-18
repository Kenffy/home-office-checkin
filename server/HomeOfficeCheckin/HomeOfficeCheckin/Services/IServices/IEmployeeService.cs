using HomeOfficeCheckin.Models;

namespace HomeOfficeCheckin.Services.IServices
{
    /// <summary>
    /// Interface for managing employee-related operations.
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Retrieves an employee by their ID asynchronously.
        /// </summary>
        /// <param name="Id">The ID of the employee to retrieve.</param>
        /// <returns>A task representing the asynchronous operation, returning the employee.</returns>
        Task<Employee> GetEployeeByIdAsync(string Id);

        /// <summary>
        /// Retrieves an employee by their username asynchronously.
        /// </summary>
        /// <param name="username">The username of the employee to retrieve.</param>
        /// <returns>A task representing the asynchronous operation, returning the employee.</returns>
        Task<Employee> GetEployeeByUserNameAsync(string username);

        /// <summary>
        /// Retrieves all employees asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, returning a read-only list of employees.</returns>
        Task<IReadOnlyList<Employee>> GetAllEployeesAsync();
    }
}
