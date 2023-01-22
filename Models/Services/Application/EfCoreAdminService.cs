using System.Security.Claims;
using System.Threading.Tasks;
using AdminArea_IdentityBase.Models.Exceptions;
using AdminArea_IdentityBase.Models.Services.Infrastructure;
using Ganss.Xss;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AdminArea_IdentityBase.Models.Services.Application
{
    public class EfCoreAdminService : IAdminService
    {
        private readonly ILogger<EfCoreAdminService> logger;
        private readonly AdminAreaDbContext dbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IEmailClient emailClient;

        public EfCoreAdminService(IHttpContextAccessor httpContextAccessor, ILogger<EfCoreAdminService> logger, IEmailClient emailClient, AdminAreaDbContext dbContext)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
            this.dbContext = dbContext;
            this.emailClient = emailClient;
        }

        public async Task SendQuestionAsync(string appEmail, string question)
        {
            string userFullName;
            string userEmail;

            userFullName = httpContextAccessor.HttpContext.User.FindFirst("FullName").Value;
            userEmail = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;

            //question = new HtmlSanitizer(allowedTags: new string[0]).Sanitize(question);
            var sanitizer = new HtmlSanitizer();
            var html = sanitizer.AllowedTags;

            question = sanitizer.Sanitize(question);

            string subject = $@"Domanda di supporto dal portale Admin Area";
            string message = $@"<p>L'utente {userFullName} (<a href=""{userEmail}"">{userEmail}</a>) ti ha inviato la seguente domanda:</p>
                                <p>{question}</p>";

            try
            {
                await emailClient.SendEmailAsync(appEmail, userEmail, subject, message);
            }
            catch
            {
                throw new SendException();
            }
        }
    }
}