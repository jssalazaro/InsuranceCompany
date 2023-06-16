using InsuranceCompnay.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceCompnay.Application.Interfaces
{
    public interface IApplication<T> : ICrud<T>
    {

    }
}
