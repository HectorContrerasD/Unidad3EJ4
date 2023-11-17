using Microsoft.AspNetCore.Mvc;

namespace FruitStore.Areas.Administrador.Controllers
{
    public class ProductosController : Controller
    {
        [HttpGet]
        [HttpPost]
        public IActionResult Index()
        {
            return View();
        }
    }
}
