using System.ComponentModel.DataAnnotations;

namespace AdminArea_IdentityBase.Models.Enums
{
    public enum Role
    {
        [Display(Name = "Amministratore")]
        Administrator,

        [Display(Name = "Utente")]
        User
    }
}