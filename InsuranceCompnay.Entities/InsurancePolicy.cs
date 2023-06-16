using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceCompnay.Entities
{  
    public class InsurancePolicy
    {
        public int Id { get; set; }
        public string InsuranceNumber { get; set; }
        public DateTime InsuranceDate { get; set; }
        public decimal InsuranceTotal { get; set; }

        }    
}
