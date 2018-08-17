using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniFirst_BL.Entities;
using UniFirst_BL.Enums;

namespace UniFirst_BL.Repository
{
    public class LocationRepository
    {
        private List<Location> locations;

        private static LocationRepository locationRepo = null;

        private LocationRepository()
        {
            locations = new List<Location>();
        }
        public static LocationRepository GetRepo()
        {
            if (locationRepo == null)
            {
                locationRepo = new LocationRepository();
            }
            return locationRepo;
        }

        public void AddLocation(int LocationId, Enums.LocationType LocationType)
        {
            if(LocationType==(Enums.LocationType.DistributionCenter))
            {
                locations.Add(new DistributionCenter());
            }
            if (LocationType == (Enums.LocationType.Branch))
            {
                locations.Add(new Branch());
            }
            locations.Add(new Location() { LocationId = LocationId, LocationType = (Enums.LocationType)(LocationType) });
        }

        public Location GetLocationById(int LocationId)
        {
            return locations.Find(x => x.LocationId == LocationId);
        }

        public bool AddBranchToDistributionCenter(int BrachId,int DistributionCenterId)
        {
            Location locObj = GetLocationById(DistributionCenterId);
            if (locObj.LocationType != LocationType.DistributionCenter)
                return false;
            (locObj as DistributionCenter).Branches.Add(new Branch() { LocationId = BrachId });
            return true;
            
        }

        private bool AddVehiclesToDistributionCenter(string Vin, int DistributionCenterId)
        {
            Location locObj = GetLocationById(DistributionCenterId);
            if (locObj.LocationType != LocationType.DistributionCenter)
                return false;
            (locObj as DistributionCenter).Vehicles.Add(new Vehicle() { Vin = Vin });
            return true;

        }

        private bool AddVehiclesToBrach(string Vin, int BranchId)
        {
                Location locObj = GetLocationById(BranchId);
            if (locObj.LocationType != LocationType.Branch)
                return false;
            (locObj as Branch).Vehicles.Add(new Vehicle() { Vin = Vin });
            return true;
        }

        public void ClearRepository()
        {
            locations.Clear();
        }
    }
}
