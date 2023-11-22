using FruitStore.Areas.Administrador.Models;
using FruitStore.Models.Entities;
using FruitStore.Repositories;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Security.Permissions;

namespace FruitStore.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    public class ProductosController : Controller
    {
        private readonly Repository<Categorias> categoriasRepository;
        private readonly ProductosRepository productosRepository;
        public ProductosController(ProductosRepository productosRepository, Repository<Categorias> categoriasRepository)
        {   
            this.productosRepository = productosRepository;
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
                vm.Productos = productosRepository.GetAll().Select(x => new ProductosModel
                {
                    Id = x.Id,
                    Nombre = x.Nombre ?? "",
                    Categoria = x.IdCategoriaNavigation?.Nombre ?? ""
                });
            }
            else
            {
                vm.Productos = productosRepository.GetProductosByCategorias(vm.IdCategoriaSeleccionada).Select(x => new ProductosModel
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
                    productosRepository.Insert(vm.Producto);
                    if (vm.Archivo == null)
                    {
                        System.IO.File.Copy("wwwroot/img_frutas/0.jpg", $"wwwroot/img_frutas/{vm.Producto.Id}.jpg");
                    }
                    else
                    {
                        System.IO.FileStream fs=
                        System.IO.File.Create($"wwwroot/img_frutas/{vm.Producto.Id}.jpg");
                        vm.Archivo.CopyTo(fs);
                    }

                }
                return RedirectToAction("Index");
            }
            return View(vm);
        }
        public IActionResult Editar(int id)
        {
            var producto = productosRepository.Get(id);
            if (producto == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                AdminAgregarProductosViewModel vm = new AdminAgregarProductosViewModel();
                vm.Producto = producto;
                vm.Categorias = categoriasRepository.GetAll().OrderBy(x => x.Nombre)
                .Select(x => new CategoriaModel
                {
                    Id = x.Id,
                    Nombre = x.Nombre ?? ""
                });
                return View(vm);
            }
           
        }
        [HttpPost]
        public IActionResult Editar(AdminAgregarProductosViewModel vm)
        {

            if (vm.Archivo != null)
            {
                if (vm.Archivo.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("", "Solo se permiten imagenes jpeg");
                }
                if (vm.Archivo.Length > 500 * 1024)
                {
                    ModelState.AddModelError("", "Solo se permiten archivos no mayores a 500Kb");
                }
                if (ModelState.IsValid)
                {
                    var producto = productosRepository.Get(vm.Producto.Id);
                    if (producto == null)
                    {
                        return RedirectToAction("index");
                    }
                    else
                    {
                        producto.Nombre = vm.Producto.Nombre;
                        producto.Precio = vm.Producto.Precio;
                        producto.Descripcion = vm.Producto.Descripcion;
                        producto.IdCategoria = vm.Producto.IdCategoria;
                        producto.UnidadMedida = vm.Producto.UnidadMedida;
                        productosRepository.Update(producto);
                    }

                }
                return RedirectToAction("Index");
            }
            return View(vm);
        }
    }
}
