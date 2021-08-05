using System.Threading.Tasks;

namespace AdminArea_IdentityBase.Models.Services.Application
{
    public interface IAdminService
    {
        Task SendQuestionAsync(string appEmail, string question);
    }
}