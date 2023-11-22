using FruitStore.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FruitStore.Repositories
{
    public class ProductosRepository : Repository<Productos>
    {
        public ProductosRepository(FruteriashopContext context) : base(context)
        {
        }
        public override IEnumerable<Productos> GetAll()
        {
            return Context.Productos.Include(x => x.IdCategoriaNavigation).OrderBy(x => x.Nombre);
        }
        public IEnumerable<Productos> GetProductosByCategorias(string Categoria)
        {
            return Context.Productos.Include(x=>x.IdCategoriaNavigation).Where(x=>x.IdCategoriaNavigation!=null && x.IdCategoriaNavigation.Nombre == Categoria).OrderBy(x=>x.Nombre);
        }
        public Productos? GetByNombre(string nombre)
        {
            return Context.Productos
                .Include(x=>x.IdCategoriaNavigation)
                .FirstOrDefault(x=>x.Nombre==nombre);
        }
        public IEnumerable<Productos> GetProductosByCategorias(int Categoria)
        {
            return Context.Productos.Include(x => x.IdCategoriaNavigation).Where(x => x.IdCategoria == Categoria).OrderBy(x => x.Nombre);
        }
    }
}
