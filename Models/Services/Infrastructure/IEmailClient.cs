using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace AdminArea_IdentityBase.Models.Services.Infrastructure
{
    public interface IEmailClient : IEmailSender
    {
        Task SendEmailAsync(string recipientEmail, string replyToEmail, string subject, string htmlMessage);
    }
}