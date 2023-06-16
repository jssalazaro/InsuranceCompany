﻿using InsuranceCompnay.Abstractions;
using InsuranceCompnay.Repository.Interfaces;

namespace InsuranceCompnay.Repository.Implementation
{
    public class Repository<T> : IRepository<T> where T : IEntity
    {
        public readonly IDbContext<T> _context;

        public Repository(IDbContext<T> context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            _context.Delete(id);
        }

        public IList<T> GetAll()
        {
            return _context.GetAll();
        }

        public T GetById(int id)
        {
            return _context.GetById(id);
        }

        public T Save(T entity)
        {
            return _context.Save(entity);
        }
    }
}
