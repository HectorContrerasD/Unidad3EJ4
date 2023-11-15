using FruitStore.Areas.Administrador.Models;
using FruitStore.Models.Entities;
using FruitStore.Repositories;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Collections.Immutable;

namespace FruitStore.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    public class CategoriasController : Controller
    {
        public CategoriasController(Repositories.Repository<Categorias> repository)
        {
             Repository = repository;
        }

        public Repository<Categorias> Repository { get; }

        public IActionResult Index()
        {
            AdminCategoriaViewModel vm = new();
            vm.Categorias = Repository.GetAll().OrderBy(x=>x.Nombre).Select(x=> new CategoriaModel
            {
                Id= x.Id,
                Nombre = x.Nombre ??""
            });
            
            return View(vm);
        }

        public IActionResult Agregar() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Agregar(Categorias categorias)
        {
            if (string.IsNullOrWhiteSpace(categorias.Nombre)) 
            {
                ModelState.AddModelError("", "Escriba el nombre de la categoría");
            }
            if (ModelState.IsValid)
            {
                Repository.Insert(categorias);
                return RedirectToAction("Index");
            }
            return View(categorias);
        }
        public IActionResult Editar(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Editar(Categorias categorias)
        {
            return View(); 
        }
        public IActionResult Eliminar(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Eliminar(Categorias categorias)
        {
            return View();
        }
    }
}
