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
                default:
                    ViewData["Title"] = "Errore";
                    return View();
            }
        }
    }
}