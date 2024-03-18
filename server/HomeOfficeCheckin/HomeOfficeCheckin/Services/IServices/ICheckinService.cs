using HomeOfficeCheckin.Models;

namespace HomeOfficeCheckin.Services.IServices
{
    /// <summary>
    /// Interface for managing home office time-related operations.
    /// </summary>
    public interface ICheckinService
    {
        /// <summary>
        /// Starts a new home office time entry asynchronously.
        /// </summary>
        /// <param name="entity">The home office time entity to start.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task StartHomeOfficeAsync(HomeOfficeTime entity);

        /// <summary>
        /// Stops an ongoing home office time entry asynchronously.
        /// </summary>
        /// <param name="entity">The home office time entity to stop.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task StopHomeOfficeAsync(HomeOfficeTime entity);

        /// <summary>
        /// Retrieves the current open home office time entry for a specific user asynchronously.
        /// </summary>
        /// <param name="Id">The ID of the user.</param>
        /// <returns>A task representing the asynchronous operation, returning the current open home office time entry.</returns>
        Task<HomeOfficeTime> GetCurrentOpenHomeOfficeTimeAsync(string Id);

        /// <summary>
        /// Retrieves the current open home office time entry for a specific user on a specific day asynchronously.
        /// </summary>
        /// <param name="Id">The ID of the user.</param>
        /// <param name="day">The date of the day.</param>
        /// <returns>A task representing the asynchronous operation, returning the current open home office time entry.</returns>
        Task<HomeOfficeTime> GetCurrentOpenHomeOfficeTimesByUserIdAsync(string Id, DateTime day);

        /// <summary>
        /// Retrieves home office time entries for a specific user and day range asynchronously.
        /// </summary>
        /// <param name="Id">The ID of the user.</param>
        /// <param name="day">The date of the day.</param>
        /// <returns>A task representing the asynchronous operation, returning a read-only list of home office time entries.</returns>
        Task<IReadOnlyList<HomeOfficeTime>> GetHomeOfficeTimesByUserIdAndDaysAsync(string Id, DateTime day);
    }
}
