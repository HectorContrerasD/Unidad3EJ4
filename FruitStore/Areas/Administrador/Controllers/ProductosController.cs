using FruitStore.Areas.Administrador.Models;
using FruitStore.Models.Entities;
using FruitStore.Repositories;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Security.Permissions;

namespace FruitStore.Areas.Administrador.Controllers
{
    [Authorize(Roles = "Administrador, Supervisor")]
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
            if (vm.Archivo != null) //Si selecciono un archivo
            {
                //MIME TYPE
                if (vm.Archivo.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("", "Solo se permiten imagenes JPG.");
                }

                if (vm.Archivo.Length > 500 * 1024)//500kb
                {
                    ModelState.AddModelError("", "Solo se permiten archivos no mayores a 500Kb");

                }
            }

            if (ModelState.IsValid)
            {
                productosRepository.Insert(vm.Producto);

                if (vm.Archivo == null) //No eligió archivo
                {
                    //Obtener el ID del producto
                    //Copiar el archivo llamado nodisponible.jpg y cambiar el nombre por el id

                    System.IO.File.Copy("wwwroot/img_frutas/0.jpg", $"wwwroot/img_frutas/{vm.Producto.Id}.jpg");
                }
                else
                {
                    System.IO.FileStream fs = System.IO.File.Create($"wwwroot/img_frutas/{vm.Producto.Id}.jpg");
                    vm.Archivo.CopyTo(fs);
                    fs.Close();
                }
                return RedirectToAction("Index");
            }

            vm.Categorias = categoriasRepository.GetAll().OrderBy(x => x.Nombre).Select(x => new CategoriaModel
            {
                Id = x.Id,
                Nombre = x.Nombre ?? "No disponible"
            });
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
                AdminAgregarProductosViewModel vm = new();
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

            if (vm.Archivo != null) //Si selecciono un archivo
            {
                //MIME TYPE
                if (vm.Archivo.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("", "Solo se permiten imagenes JPG.");
                }

                if (vm.Archivo.Length > 500 * 1024)//500kb
                {
                    ModelState.AddModelError("", "Solo se permiten archivos no mayores a 500Kb");

                }
            }

            if (ModelState.IsValid)
            {
                var producto = productosRepository.Get(vm.Producto.Id);
                if (producto == null)
                {
                    return RedirectToAction("Index");
                }

                producto.Nombre = vm.Producto.Nombre;
                producto.Precio = vm.Producto.Precio;
                producto.Descripcion = vm.Producto.Descripcion;
                producto.UnidadMedida = vm.Producto.UnidadMedida;
                producto.IdCategoria = vm.Producto.IdCategoria;


                productosRepository.Update(producto);

                //Editar la foto
                if (vm.Archivo != null)
                {
                    System.IO.FileStream fs = System.IO.File.Create($"wwwroot/img_frutas/{vm.Producto.Id}.jpg");
                    vm.Archivo.CopyTo(fs);
                    fs.Close();
                }
                return RedirectToAction("Index");
            }
            vm.Categorias = categoriasRepository.GetAll().OrderBy(x => x.Nombre).Select(x => new CategoriaModel
            {
                Id = x.Id,
                Nombre = x.Nombre ?? "No disponible"
            });
            return View(vm);
           
        }
        public IActionResult Eliminar(int id)
        {
            var producto = productosRepository.Get(id);
            if (producto == null)
            {
                return RedirectToAction("Index");
            }
            return View(producto);
        }
        [HttpPost]
        public IActionResult Eliminar(Productos p)
        {
            var producto = productosRepository.Get(p.Id);
            if (producto == null) 
            {
                return RedirectToAction("Index");
            }
            productosRepository.Delete(p.Id);
            var ruta = $"wwwroot/img_frutas/{p.Id}.jpg";
            if (System.IO.File.Exists(ruta))
            {
                System.IO.File.Delete(ruta);
            }
            return RedirectToAction("Index");
        }
    }
}
