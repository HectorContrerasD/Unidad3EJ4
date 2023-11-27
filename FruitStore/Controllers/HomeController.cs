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
                Productos = Repository.GetProductosByCategorias(Id).OrderBy(x => x.Nombre)
                .Select(x => new ProductosModel
                {
                    Nombre = x.Nombre ?? "",
                    Id = x.Id,
                    Precio = x.Precio ?? 00,
                    FechaModificacion = new FileInfo($"wwwroot/img_frutas/{x.Id}.jpg").LastWriteTime.ToString("yyyyMMddhhmm")
                })
            };
            return View(productosViewModel);
        }

        public IActionResult Ver(string Id)
        {
           Id= Id.Replace("-"," ");
            var productos = Repository.GetByNombre(Id);
            if (productos == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                VerProductoViewModel vm = new()
                {
                    Id = productos.Id,
                    Categoria = productos.IdCategoriaNavigation?.Nombre ?? "",
                    Descripcion = productos.Descripcion ?? "",
                    Nombre = productos.Nombre ?? "",
                    Precio = productos.Precio ?? 0m,
                    UnidadMedida = productos.UnidadMedida ?? ""
                };
                return View(vm);
            }
            
        }

    }
}
