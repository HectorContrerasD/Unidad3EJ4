﻿namespace FruitStore.Areas.Administrador.Models
{
    public class AdminProductosViewModel
    {
        public int IdCategoriaSeleccionada { get; set; }
        public IEnumerable<CategoriaModel> Categorias { get; set; } = null!;
        public IEnumerable<ProductosModel> Productos { get; set; } = null!;

    }
    public class ProductosModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Categoria { get; set; } = null!;

    }
}
