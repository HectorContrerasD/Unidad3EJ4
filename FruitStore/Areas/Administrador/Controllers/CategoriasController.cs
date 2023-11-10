using Microsoft.AspNetCore.Mvc;

namespace FruitStore.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    public class CategoriasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
