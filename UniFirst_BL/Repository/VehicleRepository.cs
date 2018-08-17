using System.Collections.Generic;

namespace UniFirst_BL.Repository
{
    public class VehicleRepository
    {
        private  List<Vehicle> vehicles;
        private  VehicleRepository()
        {
            vehicles = new List<Vehicle>();
        }
        private static VehicleRepository vehicleRepo = null;

        public static VehicleRepository GetRepo()
        {
            if (vehicleRepo == null)
            {
                vehicleRepo = new VehicleRepository();
            }
            return vehicleRepo;
        }
       public bool AddVehicle(string Make,string Year, string Model, string Vin,Enums.VehicleTypes vehicleTypes,Enums.VehicleStatus vehicleStatus)
        {
          if(IsValidVin(Vin) && IsValidYear(Year))
           {
                vehicles.Add(new Vehicle() {Make=Make,Vin=Vin,Model=Model,Year=Year, VehicleType=vehicleTypes,VehicleStatus=vehicleStatus });
                return true;
           }
            return false;
        }

        public Vehicle GetVehicleByVinNumber(string Vin)
        {
            return vehicles.Find(x => x.Vin == Vin);
        }

        public void ClearRepository()
        {
            vehicles.Clear();
        }
        private bool IsValidYear(string Year)
        {
            if(Year.Length<4)
            {
                return false;
            }

            var isNumeric = int.TryParse(Year, out int n);

            if (!isNumeric)
            {
                return false;
            }
            return true;
        }
        private bool IsValidVin(string Vin)
        {
            int alfaCounter = 0;
            // Check size to be 24
            if(Vin.Length<24)
            {
               
                return false;
            }

            // Last 5 chars to be numeric
            var isNumeric = int.TryParse(Vin.Substring(19, 5), out int n);
            if(!isNumeric)
            {
              return false;
            }

            foreach(char c in Vin)
            {
                if(char.IsLetter(c))
                {
                    alfaCounter = alfaCounter + 1;
                }
            }
            if (alfaCounter < 8)
            {
                return false;
            }
                return true;
        }
        
    }
}
