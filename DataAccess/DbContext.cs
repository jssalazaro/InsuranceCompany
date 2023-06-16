using InsuranceCompnay.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace InsuranceCompnay.DataAccess
{
    public class DbContext<T> : IDbContext<T> where T : class, IEntity
    {
        public DbSet<T> _items;

        public readonly ApiDbContext _context;
        public DbContext(ApiDbContext context)
        {
            _context = context;
            _items = context.Set<T>();
        }

        public void Delete(int id)
        {
           
        }

        public IList<T> GetAll()
        {
            return _items.ToList();
        }

        public T GetById(int id)
        {
            return _items.Where(u => u.Id.Equals(id)).FirstOrDefault();
        }

        public T Save(T entity)
        {
            _items.Add(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
