using HomeOfficeCheckin.Models;

namespace HomeOfficeCheckin.Services.IServices
{
    /// <summary>
    /// Interface for managing email-related operations.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email notification to the HR department asynchronously.
        /// </summary>
        /// <param name="homeOffice">The home office time entry associated with the email notification.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SendEmailToHRAsync(HomeOfficeTime homeOffice);
    }
}
