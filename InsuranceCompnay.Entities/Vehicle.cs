using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceCompnay.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Plate { get; set; }
        public string Model { get; set; }
        public bool HasInspection { get; set; }
    }
}
