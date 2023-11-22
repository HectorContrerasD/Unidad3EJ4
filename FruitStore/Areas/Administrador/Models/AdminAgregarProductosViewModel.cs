using FruitStore.Models.Entities;

namespace FruitStore.Areas.Administrador.Models
{
    public class AdminAgregarProductosViewModel
    {
        public Productos? Producto { get; set; } = null!;
        public IEnumerable<CategoriaModel> Categorias { get; set; } = null!;
        public IFormFile Archivo { get; set; } = null!;

    }
}
