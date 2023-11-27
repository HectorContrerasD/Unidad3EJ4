using FruitStore.Models.Entities;

namespace FruitStore.Areas.Administrador.Models
{
    public class AdminAgregarProductosViewModel
    {
        public Productos Producto { get; set; } =new();
        public IEnumerable<CategoriaModel>? Categorias { get; set; }
        public IFormFile? Archivo { get; set; }

    }
}
