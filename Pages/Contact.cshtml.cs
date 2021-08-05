using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AdminArea_IdentityBase.Models.Options;
using AdminArea_IdentityBase.Models.Services.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace AdminArea_IdentityBase.Pages
{
    public class ContactModel : PageModel
    {
        private readonly IOptionsMonitor<UsersOptions> _usersOptions;

        public ContactModel(
            IOptionsMonitor<UsersOptions> usersOptions)
        {
            _usersOptions = usersOptions;
        }

        [Required(ErrorMessage = "Il testo della domanda è obbligatorio")]
        [Display(Name = "La tua domanda")]
        [BindProperty]
        public string Question { get; set; }

        public IActionResult OnGetAsync()
        {
            try
            {
                ViewData["Title"] = $"Contatti";
                return Page();
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> OnPostAsync([FromServices] IAdminService adminService)
        {
            string SupportEmail = _usersOptions.CurrentValue.NotificationEmailSupportRecipient;

            if (ModelState.IsValid)
            {
                await adminService.SendQuestionAsync(SupportEmail, Question);
                TempData["ConfirmationMessage"] = "La tua domanda è stata inviata";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return OnGetAsync();
            }
        }
    }
}