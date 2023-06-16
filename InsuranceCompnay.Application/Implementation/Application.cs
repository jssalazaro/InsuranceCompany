using InsuranceCompnay.Abstractions;
using InsuranceCompnay.Application.Interfaces;
using InsuranceCompnay.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceCompnay.Application.Implementation
{
    public class Application<T> : IApplication<T> where T: IEntity
    {
        public readonly IRepository<T> _repository;
        public Application(IRepository<T> repository) 
        { 
            _repository = repository;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public IList<T> GetAll()
        {
            return _repository.GetAll();
        }

        public T GetById(int id)
        {
            return _repository.GetById(id);
        }

        public T Save(T entity)
        {
            return _repository.Save(entity);
        }
    }
}
