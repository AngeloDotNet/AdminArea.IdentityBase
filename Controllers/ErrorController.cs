using AdminArea_IdentityBase.Models.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AdminArea_IdentityBase.Controllers
{
    public class ErrorController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            switch(feature.Error)
            {
                case SendException exc:
                    ViewData["Title"] = "Non è stato possibile inviare il messaggio, riprova più tardi";
                    Response.StatusCode = 500;
                    return View();
                    
                default:
                    ViewData["Title"] = "Errore";
                    return View();
            }
        }
    }
}