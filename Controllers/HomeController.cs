using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminArea_IdentityBase.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            ViewData["Title"] = "Benvenuto in Admin Area";
            return View();
        }
    }
}