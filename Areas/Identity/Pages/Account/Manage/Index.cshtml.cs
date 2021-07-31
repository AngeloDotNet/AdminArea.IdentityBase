using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AdminArea_IdentityBase.Models.Entities;

namespace AdminArea_IdentityBase.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
	    {
	        [Phone(ErrorMessage = "Deve essere un numero di telefono valido")]
	        [Display(Name = "Numero di telefono")]
	        public string PhoneNumber { get; set; }
	 
	        [Display(Name = "Nome completo")]
	        public string FullName { get; set; }
	    }


        private async Task LoadAsync(ApplicationUser user)
	    {
	        var userName = await _userManager.GetUserNameAsync(user);
	        var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
	 
            Username = userName;
	 
	        Input = new InputModel
	        {
	            PhoneNumber = phoneNumber,
	            FullName = user.FullName
            };
	    }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Non è stato possibile trovare il profilo utente con ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Non è stato possibile trovare il profilo utente con ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            user.FullName = Input.FullName;

            IdentityResult result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
	        {
	            StatusMessage = "Si è verificato un errore imprevisto nell'impostare il nome completo.";
	            return RedirectToPage();
	        }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Si è verificato un errore imprevisto nell'impostare il numero di telefono.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Il tuo profilo è stato aggiornato";
            return RedirectToPage();
        }
    }
}
