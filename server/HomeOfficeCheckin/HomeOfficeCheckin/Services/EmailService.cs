using HomeOfficeCheckin.Data;
using HomeOfficeCheckin.Models;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using HomeOfficeCheckin.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace HomeOfficeCheckin.Services
{
    /// <summary>
    /// Service class responsible for sending email notifications related to home office activities.
    /// </summary>
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly HomeOfficeTimeDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailService"/> class.
        /// </summary>
        /// <param name="context">The database context for accessing employee data.</param>
        /// <param name="emailSettings">The email settings.</param>
        public EmailService(HomeOfficeTimeDbContext context, IOptions<EmailSettings> emailSettings)
        {
            _context = context;
            _emailSettings = emailSettings.Value;
        }

        /// <summary>
        /// Sends an email notification to the HR department asynchronously when a home office session ends.
        /// </summary>
        /// <param name="homeOffice">The home office time entry associated with the email notification.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SendEmailToHRAsync(HomeOfficeTime homeOffice)
        {
            // Get user
            var user = await _context.Employees.FirstOrDefaultAsync(employee => employee.Id == homeOffice.UserId);

            // Email subject
            string subject = "Home Office Ended";

            // Email body
            string body = $"User {user.UserName} ended home office.\nStart Time: {homeOffice.StartTime}\nEnd Time: {homeOffice.EndTime}";

            // Configure SMTP client
            SmtpClient smtpClient = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword)
            };

            // Create and send email message
            MailMessage mailMessage = new MailMessage(_emailSettings.SenderEmail, _emailSettings.SenderEmail, subject, body);
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
