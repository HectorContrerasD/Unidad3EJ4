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
            return View();
        }
        [HttpPost]
        public IActionResult Agregar(AdminAgregarProductosViewModel vm)
        {
            return View();
        }
    }
}
