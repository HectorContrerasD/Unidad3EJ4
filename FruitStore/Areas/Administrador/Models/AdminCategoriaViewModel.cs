namespace FruitStore.Areas.Administrador.Models
{
    public class AdminCategoriaViewModel
    {
        public IEnumerable<CategoriaModel> Categorias { get; set; } = null!;
    }
    public class CategoriaModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
