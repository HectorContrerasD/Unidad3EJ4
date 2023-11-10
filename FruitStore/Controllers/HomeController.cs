using FruitStore.Models.Entities;
using FruitStore.Models.ViewModels;
using FruitStore.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FruitStore.Controllers
{
    public class HomeController : Controller
    {
        ProductosRepository Repository { get; }
        public HomeController(ProductosRepository repository)
        {
              Repository = repository;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Productos(string Id)
        {
            Id = Id.Replace("-"," ");
            ProductosViewModel productosViewModel = new ProductosViewModel()
            {
                Categoria = Id,
                Productos = Repository.GetAll().Where(x=>x.IdCategoriaNavigation?.Nombre == Id).OrderBy(x=>x.Nombre)
                .Select(x=>new ProductosModel
                {
                    Nombre= x.Nombre??"",
                    Id = x.Id,
                    Precio = x.Precio ?? 00
                })
            };
            return View(productosViewModel);
        }

        public IActionResult Ver()
        {
            return View();
        }

    }
}
