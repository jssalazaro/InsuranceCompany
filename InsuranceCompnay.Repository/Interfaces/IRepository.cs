using InsuranceCompnay.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceCompnay.Repository.Interfaces
{
    public interface IRepository<T> : ICrud<T>
    {
        
    }
}
