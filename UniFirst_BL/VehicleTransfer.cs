using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniFirst_BL.Repository;
using UniFirst_BL.Enums;
using UniFirst_BL.Entities;

namespace UniFirst_BL
{
    public class VehicleTransfer
    {

        public VehicleTransfer()
        {

        }

        public bool Transfer(int FromLocationId, int ToLocationId, string Vin)
        {
            Vehicle vehicleObj  =  VehicleRepository.GetRepo().GetVehicleByVinNumber(Vin);
            Location FromLocObj =  LocationRepository.GetRepo().GetLocationById(FromLocationId);
            Location ToLocObj   =  LocationRepository.GetRepo().GetLocationById(ToLocationId);
            if(vehicleObj==null)
            {
                Console.WriteLine("Invalid VIN");
                return false;
            }

            if (vehicleObj.VehicleStatus != VehicleStatus.StandBy)
            {
                Console.WriteLine("Vehicle is not in StandBy Status");
                return false;
            }

            if (FromLocObj==null || ToLocObj==null)
            {
                Console.WriteLine("Invalid Location provided.");
                return false;
            }

            
            if (vehicleObj.VehicleType == Enums.VehicleTypes.Semi)
            {
                if ((ToLocObj.LocationType != LocationType.DistributionCenter) || (FromLocObj.LocationType != LocationType.DistributionCenter))
                {
                    Console.WriteLine("Semi can be moved only between distrubution centers");
                    return false;
                }
            }
            
            return true;
        }
    }
}
