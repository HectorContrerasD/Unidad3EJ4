using FruitStore.Models.Entities;

namespace FruitStore.Repositories
{
    public class Repository<T> where T : class
    {
        public Repository(FruteriashopContext context)
        {
            Context = context;
        }

        public FruteriashopContext Context { get; }

        public virtual IEnumerable<T> GetAll()
        {
            return 
        }
    }
}
