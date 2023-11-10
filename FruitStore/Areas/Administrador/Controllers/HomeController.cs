using Microsoft.AspNetCore.Mvc;

namespace FruitStore.Areas.Administrador.Controllers
{
        [Area("Administrador")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
