using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceCompnay.Entities
{
    public class Customer : Entity
    {
        public string Name { get; set; }
        public string? Address { get; set; }
        public int IdCity { get; set; }
    }
}
