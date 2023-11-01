using Microsoft.AspNetCore.Mvc;

namespace FruitStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Productos()
        {
            return View();
        }

        public IActionResult Ver()
        {
            return View();
        }

    }
}
