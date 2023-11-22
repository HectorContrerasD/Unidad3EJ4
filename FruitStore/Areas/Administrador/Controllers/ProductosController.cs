using FruitStore.Areas.Administrador.Models;
using FruitStore.Models.Entities;
using FruitStore.Repositories;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace FruitStore.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    public class ProductosController : Controller
    {
        private readonly Repository<Categorias> categoriasRepository;
        private readonly ProductosRepository productsRepository;
        public ProductosController(ProductosRepository productosRepository, Repository<Categorias> categoriasRepository)
        {   
            this.productsRepository = productosRepository;
            this.categoriasRepository = categoriasRepository;
                
        }
        [HttpGet]
        [HttpPost]
        public IActionResult Index(AdminProductosViewModel vm)
        {
            vm.Categorias = categoriasRepository.GetAll().Select(x => new CategoriaModel
            {
                Id = x.Id,
                Nombre = x.Nombre??""
            });
            if (vm.IdCategoriaSeleccionada ==0)
            {
                vm.Productos = productsRepository.GetAll().Select(x => new ProductosModel
                {
                    Id = x.Id,
                    Nombre = x.Nombre ?? "",
                    Categoria = x.IdCategoriaNavigation?.Nombre ?? ""
                });
            }
            else
            {
                vm.Productos = productsRepository.GetProductosByCategorias(vm.IdCategoriaSeleccionada).Select(x => new ProductosModel
                {
                    Id = x.Id,
                    Nombre = x.Nombre ?? "",
                    Categoria = x.IdCategoriaNavigation?.Nombre ?? ""
                });
            }
            return View(vm);
        }
        public IActionResult Agregar()
        {
            AdminAgregarProductosViewModel vm = new();
            vm.Categorias = categoriasRepository.GetAll().OrderBy(x => x.Nombre)
                .Select(x=> new CategoriaModel
                {
                    Id = x.Id,
                    Nombre = x.Nombre??""
                });
            return View(vm);
        }
        [HttpPost]
        public IActionResult Agregar(AdminAgregarProductosViewModel vm)
        {
            if (vm.Archivo != null)
            {
                if(vm.Archivo.ContentType!= "image/jpeg")
                {
                    ModelState.AddModelError("", "Solo se permiten imagenes jpeg");
                }
                if (vm.Archivo.Length >500*1024)
                {
                    ModelState.AddModelError("", "Solo se permiten archivos no mayores a 500Kb");
                }
                if (ModelState.IsValid)
                {
                    productsRepository.Insert(vm.Producto);
                    if (vm.Archivo == null)
                    {

                    }

                }
            }
            return View();
        }
    }
}
