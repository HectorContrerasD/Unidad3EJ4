using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FruitStore.Areas.Administrador.Controllers
{
    [Authorize(Roles = "Administrador, Supervisor")]
        [Area("Administrador")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
