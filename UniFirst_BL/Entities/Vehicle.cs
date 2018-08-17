using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniFirst_BL.Enums;

namespace UniFirst_BL
{
    public class Vehicle
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; } 
        public string Vin { get; set; }
        public VehicleTypes VehicleType { get; set; }
        public VehicleStatus VehicleStatus { get; set; }
    }
}
