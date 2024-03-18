using HomeOfficeCheckin.Data;
using HomeOfficeCheckin.Models;
using HomeOfficeCheckin.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace HomeOfficeCheckin.Services
{
    /// <summary>
    /// Service class responsible for managing home office time entries.
    /// </summary>
    public class CheckinService : ICheckinService
    {
        private readonly HomeOfficeTimeDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckinService"/> class.
        /// </summary>
        /// <param name="context">The database context for accessing home office time entries.</param>
        public CheckinService(HomeOfficeTimeDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Starts a new home office session asynchronously.
        /// </summary>
        /// <param name="entity">The home office time entry to be added.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task StartHomeOfficeAsync(HomeOfficeTime entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Stops the current home office session asynchronously.
        /// </summary>
        /// <param name="entity">The home office time entry to be updated.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task StopHomeOfficeAsync(HomeOfficeTime entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves the current open home office session for a user asynchronously.
        /// </summary>
        /// <param name="Id">The ID of the user.</param>
        /// <returns>A task representing the asynchronous operation, returning the current open home office time entry.</returns>
        public async Task<HomeOfficeTime> GetCurrentOpenHomeOfficeTimeAsync(string Id)
        {
            return await _context.HomeOfficeTimes.FirstOrDefaultAsync(t => t.UserId == Id && t.EndTime == null);
        }

        /// <summary>
        /// Retrieves the current open home office session for a user on a specific day asynchronously.
        /// </summary>
        /// <param name="Id">The ID of the user.</param>
        /// <param name="day">The specific day to check for an open home office session.</param>
        /// <returns>A task representing the asynchronous operation, returning the current open home office time entry.</returns>
        public async Task<HomeOfficeTime> GetCurrentOpenHomeOfficeTimesByUserIdAsync(string Id, DateTime day)
        {
            return await _context.HomeOfficeTimes.FirstOrDefaultAsync(t => t.UserId == Id && t.CreatedAt.Date == day.Date && string.IsNullOrEmpty(t.EndTime));
        }

        /// <summary>
        /// Retrieves home office time entries for a user on a specific day asynchronously.
        /// </summary>
        /// <param name="Id">The ID of the user.</param>
        /// <param name="day">The specific day to retrieve home office time entries for.</param>
        /// <returns>A task representing the asynchronous operation, returning a read-only list of home office time entries.</returns>
        public async Task<IReadOnlyList<HomeOfficeTime>> GetHomeOfficeTimesByUserIdAndDaysAsync(string Id, DateTime day)
        {
            return await _context.HomeOfficeTimes.Where(t => t.UserId == Id && t.CreatedAt.Date == day.Date).ToListAsync();
        }
    }
}
