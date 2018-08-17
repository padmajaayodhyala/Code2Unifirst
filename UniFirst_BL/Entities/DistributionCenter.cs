using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniFirst_BL.Entities
{
    public class DistributionCenter: Location
    {
        public List<Vehicle> Vehicles { get; set; }
        public List<Branch> Branches { get; set; }
    }
}
