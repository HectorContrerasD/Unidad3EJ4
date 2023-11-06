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
            return Context.Set<T>();
        } 
        public virtual T? Get(object Id)
        {
            return Context.Find<T>(Id);
        }
        public virtual void Insert(T entity) 
        {
             Context.Add(entity);
            Context.SaveChanges();
        }
        public virtual void Update(T entity)
        {
            Context.Update(entity);
            Context.SaveChanges();
        }
        public virtual void Delete(object Id) 
        { 
            var entity = Context.Find<T>(Id);
            if (entity != null) 
            {
                Delete(entity);
            }
        }
    }
}
